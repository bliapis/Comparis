using Comparis.Domain.Interfaces.Queries;
using Comparis.Domain.Interfaces.Repositories;
using Comparis.Persistence;
using Comparis.Persistence.Commands;
using Comparis.Persistence.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Comparis.Bootstrap.Providers
{
    public static class PersistenceServicesConfiguration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ComparisContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ComparisConnectionString")));

            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaymentQuery, PaymentQuery>();

            return services;
        }
    }
}