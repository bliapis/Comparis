using System;

namespace Comparis.API.Requests.Payment
{
    public class ProccessPaymentRequest
    {
        public Guid AccountFrom { get; set; }
        public Guid AccountTo { get; set; }
        public decimal Amount { get; set; }
    }
}
