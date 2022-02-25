using Comparis.CrossCutting.Notification;
using FluentValidation;
using MediatR;
using OpenTracing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Comparis.Application.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
            where TRequest : MediatR.IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ITracer _tracer;
        private readonly IMessageManager _messageManager;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators,
            ITracer tracer,
            IMessageManager messageManager)
        {
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
            _tracer = tracer ?? throw new ArgumentNullException(nameof(tracer));
            _messageManager = messageManager ?? throw new ArgumentNullException(nameof(messageManager));
        }

        public async Task<TResponse> Handle(TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var span = _tracer.ActiveSpan;

            span.SetOperationName("Validation-Behaviour");

            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

                if (failures.Count != 0)
                {
                    foreach(var error in failures)
                    {
                        span.Log($"Error to validate handler. Code: {error.ErrorCode} - Message: {error.ErrorMessage}");

                        _messageManager.Add(new Notification(error.ErrorCode, error.ErrorMessage));
                    }
                }
            }

            return await next();
        }
    }
}