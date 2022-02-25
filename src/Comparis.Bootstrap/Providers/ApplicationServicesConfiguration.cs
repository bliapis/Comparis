using Comparis.Application.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Comparis.Bootstrap.Providers
{
    public static class ApplicationServicesConfiguration
    {
        private const string APPLICATION_NAMESPACE = "Comparis.Application";

        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("Comparis.Application");

            services.AddAutoMapper(assembly);

            services.AddMediatR(assembly);
            services.AddValidatorsFromAssembly(assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));

            return services;
        }
    }
}
