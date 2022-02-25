using AutoMapper;
using Comparis.API.Requests.Payment;
using Comparis.API.Responses.Payment;
using Comparis.Application.Models;
using Comparis.Application.UseCases.Payment.Commands.ProccessPayment;
using Comparis.Application.UseCases.Payment.Queries.GetById;
using Comparis.CrossCutting.Notification;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenTracing;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Comparis.API.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ITracer _tracer;

        public PaymentController(IMediator mediator,
            IMapper mapper,
            ITracer tracer,
            IMessageManager messageManager)
            : base(messageManager, tracer)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _tracer = tracer ?? throw new ArgumentNullException(nameof(tracer));
        }

        [HttpGet("{paymentId}", Name = "PaymentId")]
        public async Task<IActionResult> GetPaymentById(Guid paymentId)
        {
            var span = _tracer.ActiveSpan;
        
            span.SetOperationName("payment-GetPaymentById");
        
            span.Log("Starting");
        
            var query = new GetByIdQuery(paymentId);
        
            var result = await _mediator.Send(query);
        
            var response = _mapper.Map<PaymentResponse>(result);
        
            span.Log("Finishing");
        
            return Response(response);
        }

        [HttpPost("run-payment")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> ProcessPayment(ProccessPaymentRequest request)
        {
            var span = _tracer.ActiveSpan;

            span.SetOperationName("payment-GetPaymentById");

            span.Log("Starting");

            var command = _mapper.Map<ProccessPaymentCommand>(request);

            var result = await _mediator.Send(command);

            span.Log("Finishing");

            return Response($"Payment processed => {result}");
        }
    }
}
