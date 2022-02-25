using System;

namespace Comparis.Domain.Entities
{
    public class Balance : BaseEntity
    {
        public Guid AccountId { get; set; }
        public decimal Amount { get; set; }
    }
}