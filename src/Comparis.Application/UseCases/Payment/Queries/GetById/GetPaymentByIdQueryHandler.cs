using AutoMapper;
using Comparis.Application.Models;
using Comparis.Domain.Interfaces.Queries;
using Comparis.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using OpenTracing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Comparis.Application.UseCases.Payment.Queries.GetById
{
    public class GetByIdHandler : IRequestHandler<GetByIdQuery, PaymentModel>
    {
        private readonly IPaymentQuery _paymentQuery;
        private readonly IMapper _mapper;
        private readonly ILogger<GetByIdHandler> _logger;
        private readonly ITracer _tracer;

        public GetByIdHandler(
            IPaymentQuery paymentQuery,
            IMapper mapper,
            ILogger<GetByIdHandler> logger,
            ITracer tracer)
        {
            _paymentQuery = paymentQuery ?? throw new ArgumentNullException(nameof(paymentQuery));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _tracer = tracer ?? throw new ArgumentNullException(nameof(tracer));
        }

        public async Task<PaymentModel> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting GetByIdHandler - Handler");

            var span = _tracer.ActiveSpan;

            span.SetOperationName("Starting GetByIdHandler - Handler");

            span.Log("Starting");

            var databasePaymentEntity = await _paymentQuery.GetByIdAsync(request.PaymentId);

            _logger.LogInformation("Finishing GetById - Handler");
            span.Log("Finishing GetById - Handler");

            return _mapper.Map<PaymentModel>(databasePaymentEntity);
        }
    }
}