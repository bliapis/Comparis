using System;

namespace Comparis.API.Responses.Payment
{
    public class PaymentResponse
    {
        public Guid Id { get; set; }
        public Guid AccountFrom { get; set; }
        public Guid AccountTo { get; set; }
        public decimal Amount { get; set; }
    }
}
