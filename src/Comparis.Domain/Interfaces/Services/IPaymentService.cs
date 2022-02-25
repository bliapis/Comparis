using Comparis.Domain.Entities;
using System.Threading.Tasks;

namespace Comparis.Domain.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<Payment> ProcessAsync(Payment payment);
    }
}
