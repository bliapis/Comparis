using Comparis.CrossCutting.Notification;
using Comparis.Domain.Interfaces.Services;
using Comparis.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Comparis.Bootstrap.Providers
{
    public static class DomainServicesConfiguration
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ILimitService, LimitService>();
            services.AddScoped<IBalanceService, BalanceService>();

            services.AddScoped<IMessageManager, MessageManager>();

            return services;
        }
    }
}