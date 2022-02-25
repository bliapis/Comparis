using Comparis.Domain.Entities;
using Comparis.Domain.Interfaces.Queries;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Comparis.Persistence.Queries
{
    public class PaymentQuery : IPaymentQuery
    {
        protected readonly ComparisContext _dbContext;

        public PaymentQuery(ComparisContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<Payment> GetByIdAsync(Guid paymentId)
        {
            //TODO: Use dapper

            var payment = await _dbContext.Payments.FirstOrDefaultAsync(p => p.Id == paymentId);
            
            return payment;
        }
    }
}