namespace SimplePollManager.Api.IntegrationTests.Infrastructure
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.FeatureManagement;
    using SimplePollManager.Api.Infrastructure.Filters;
    using SimplePollManager.Api.IntegrationTests.Infrastructure.DataFeeders;
    using SimplePollManager.Core.Extensions;
    using SimplePollManager.Database;

    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration)
            : base(configuration)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services
                .AddHttpContextAccessor()
                .AddMvcCore(options =>
                {
                    options.Filters.Add<ValidateModelStateFilter>();
                })
                .AddDataAnnotations()
                .SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddCoreComponents();

            services.AddFeatureManagement();

            services.AddDbContext<PollContext>(options =>
            {
                options.UseInMemoryDatabase("polls");
            });
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var pollContext = app.ApplicationServices.GetService<PollContext>();
            PollContextDataFeeder.Feed(pollContext);

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
