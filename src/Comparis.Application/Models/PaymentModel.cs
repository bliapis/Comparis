using System;

namespace Comparis.Application.Models
{
    public class PaymentModel
    {
        public Guid Id { get; set; }
        public Guid AccountFrom { get; set; }
        public Guid AccountTo { get; set; }
        public decimal Amount { get; set; }
    }
}