using System;
using System.Threading.Tasks;

namespace Comparis.Domain.Interfaces.Services
{
    public interface IBalanceService
    {
        Task<bool> ValidateBalanceAsync(Guid accountId);
    }
}
