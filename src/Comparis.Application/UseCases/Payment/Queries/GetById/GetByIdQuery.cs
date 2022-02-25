using Comparis.Application.Models;
using MediatR;
using System;

namespace Comparis.Application.UseCases.Payment.Queries.GetById
{
    public class GetByIdQuery : IRequest<PaymentModel>
    {
        public Guid PaymentId { get; set; }

        public GetByIdQuery(Guid paymentId)
        {
            PaymentId = paymentId;
        }
    }
}