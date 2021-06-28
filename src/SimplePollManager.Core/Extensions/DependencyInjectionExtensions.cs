namespace SimplePollManager.Core.Extensions
{
    using Microsoft.Extensions.DependencyInjection;

    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddCoreComponents(this IServiceCollection services)
        {
            return services;
        }
    }
}
