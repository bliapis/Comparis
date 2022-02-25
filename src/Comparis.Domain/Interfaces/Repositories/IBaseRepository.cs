using Comparis.Domain.Entities;
using System.Threading.Tasks;

namespace Comparis.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> AddAsync(T entity);
    }
}