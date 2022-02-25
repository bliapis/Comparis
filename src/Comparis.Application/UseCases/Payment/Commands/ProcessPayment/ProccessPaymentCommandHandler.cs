using AutoMapper;
using Comparis.Domain.Interfaces.Repositories;
using Comparis.Domain.Interfaces.Services;
using Entities = Comparis.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using OpenTracing;
using Comparis.CrossCutting.Notification;

namespace Comparis.Application.UseCases.Payment.Commands.ProccessPayment
{
    public class ProccessPaymentCommandHandler : IRequestHandler<ProccessPaymentCommand, Guid>
    {
        private readonly IPaymentService _paymentService;
        private readonly IPaymentRepository _paymentRespository;
        private readonly IMapper _mapper;
        private readonly ILogger<ProccessPaymentCommandHandler> _logger;
        private readonly ITracer _tracer;
        private readonly IMessageManager _messagemanager;

        public ProccessPaymentCommandHandler(IPaymentService paymentService,
            IPaymentRepository paymentRespository,
            IMapper mapper,
            ILogger<ProccessPaymentCommandHandler> logger,
            ITracer tracer,
            IMessageManager messagemanager)
        {
            _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
            _paymentRespository = paymentRespository ?? throw new ArgumentNullException(nameof(paymentRespository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _tracer = tracer ?? throw new ArgumentNullException(nameof(tracer));
            _messagemanager = messagemanager ?? throw new ArgumentNullException(nameof(messagemanager));
        }

        public async Task<Guid> Handle(ProccessPaymentCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting ProccessPaymentCommandHandler");

            var span = _tracer.ActiveSpan;

            span.SetOperationName("Handle-ProccessPaymentCommandHandler");

            span.Log("Starting");

            var paymentEntity = _mapper.Map<Entities.Payment>(request);

            await _paymentService.ProcessAsync(paymentEntity);

            if (_messagemanager.HasNotifications())
            {
                return new Guid();
            }

            var databasePaymentEntity = await _paymentRespository.AddAsync(paymentEntity);

            _logger.LogInformation("Finishing ProccessPaymentCommandHandler");
            span.Log("Finishing ProccessPaymentCommandHandler");

            return databasePaymentEntity.Id;
        }
    }
}
