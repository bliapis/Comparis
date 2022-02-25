using Comparis.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Comparis.Persistence
{
    public class ComparisContext : DbContext
    {
        public ComparisContext(DbContextOptions<ComparisContext> options) : base(options)
        {
        }
    
        public DbSet<Payment> Payments { get; set; }
    }
}
