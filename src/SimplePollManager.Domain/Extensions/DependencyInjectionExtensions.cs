namespace SimplePollManager.Domain.Extensions
{
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDomainComponents(this IServiceCollection services)
        {
            return services;
        }
    }
}
