using MediatR;
using System;

namespace Comparis.Application.UseCases.Payment.Commands.ProccessPayment
{
    public class ProccessPaymentCommand : IRequest<Guid>
    {
        public ProccessPaymentCommand(
            Guid accountFrom,
            Guid accountTo,
            decimal amount)
        {
            AccountFrom = accountFrom;
            AccountTo = accountTo;
            Amount = amount;
        }

        public Guid AccountFrom { get; private set; }
        public Guid AccountTo { get; private set; }
        public decimal Amount { get; private set; }
    }
}