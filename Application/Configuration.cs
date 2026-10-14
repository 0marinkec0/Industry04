using Application.Common;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class Configuration
    {
        public static IServiceCollection AddApplicationSerivces(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            var assembly = typeof(FluentValidation.IValidator).Assembly;

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
