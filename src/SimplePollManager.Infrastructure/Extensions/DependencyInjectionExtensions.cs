namespace SimplePollManager.Infrastructure.Extensions
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using SimplePollManager.Application.Common.Interfaces;
    using SimplePollManager.Domain;
    using SimplePollManager.Infrastructure.Persistence;
    using SimplePollManager.Infrastructure.Services;


    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfrastructureComponents(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<PollDbContext>(options => options.UseInMemoryDatabase("SimplePollManager"));
            }
            else
            {
                services.AddDbContext<PollDbContext>(options =>
                    options.UseSqlServer(
                        connectionString,
                        b => b.MigrationsAssembly(typeof(PollDbContext).Assembly.FullName)));
            }

            services.AddDbContext<PollDbContext>(options => options.UseSqlServer());

            services.AddHealthChecks()
                .AddSqlServer(connectionString);

            services.AddScoped<IPollDbContext>(provider => provider.GetService<PollDbContext>());

            services.AddTransient<IDateTime, DateTimeService>();

            return services;
        }
    }
}
