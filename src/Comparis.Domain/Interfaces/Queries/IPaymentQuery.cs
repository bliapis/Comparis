using Comparis.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Comparis.Domain.Interfaces.Queries
{
    public interface IPaymentQuery
    {
        Task<Payment> GetByIdAsync(Guid paymentId);
    }
}