using Comparis.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace Comparis.Domain.Interfaces.Services
{
    public interface ILimitService
    {
        Task<bool> ValidateLimitAsync(Guid accountId);
    }
}