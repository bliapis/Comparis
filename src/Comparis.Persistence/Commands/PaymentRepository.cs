using Comparis.Domain.Entities;
using Comparis.Domain.Interfaces.Repositories;

namespace Comparis.Persistence.Commands
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(ComparisContext dbContext) : base(dbContext)
        {
        }
    }
}