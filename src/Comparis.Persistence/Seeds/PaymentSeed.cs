using Comparis.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comparis.Persistence.Seeds
{
    public class PaymentSeed
    {
        public static async Task SeedAsync(ComparisContext comparisContext, ILogger<PaymentSeed> logger)
        {
            if (!comparisContext.Payments.Any())
            {
                comparisContext.Payments.AddRange(GetPreconfiguredPayments());

                await comparisContext.SaveChangesAsync();

                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(ComparisContext).Name);
            }
        }

        private static IEnumerable<Payment> GetPreconfiguredPayments()
        {
            return new List<Payment>
            {
                new Payment() { Id = Guid.NewGuid(), AccountFrom = Guid.NewGuid(), AccountTo = Guid.NewGuid(), Amount = 10 }
            };
        }
    }
}