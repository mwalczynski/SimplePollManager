namespace SimplePollManager.Application.Common.Extensions
{
    using System.Reflection;
    using FluentValidation;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using SimplePollManager.Application.Common.Behavior;

    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddApplicationComponents(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
