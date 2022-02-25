using System;

namespace Comparis.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public Guid AccountFrom { get; set; }
        public Guid AccountTo { get; set; }
        public decimal Amount { get; set; }
    }
}
