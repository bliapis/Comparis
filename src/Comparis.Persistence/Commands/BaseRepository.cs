using Comparis.Domain.Entities;
using Comparis.Domain.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace Comparis.Persistence.Commands
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly ComparisContext _dbContext;

        public BaseRepository(ComparisContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);

            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}