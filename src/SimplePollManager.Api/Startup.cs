namespace SimplePollManager.Api
{
    using HealthChecks.UI.Client;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using SimplePollManager.Api.Infrastructure.Filters;
    using SimplePollManager.Core;
    using SimplePollManager.Core.Extensions;
    using SimplePollManager.Database;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public virtual void ConfigureServices(IServiceCollection services)
        {
            services
                .AddHttpContextAccessor()
                .AddRouting(options => options.LowercaseUrls = true)
                .AddMvcCore(options =>
                {
                    options.Filters.Add<HttpGlobalExceptionFilter>();
                    options.Filters.Add<ValidateModelStateFilter>();
                })
                .AddApiExplorer()
                .AddDataAnnotations()
                .SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddHealthChecks()
                .AddSqlServer(this.configuration.GetConnectionString("MsSqlDb"));

            services.AddSwaggerGen();


            services.AddDbContextPool<PollContext>(options => options.UseSqlServer(this.configuration.GetConnectionString("MsSqlDb")));
            services.AddScoped<IPollContext, PollContext>();

            services.AddCoreComponents();
        }

        public virtual void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Simple Poll Manager V1");
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                });
            });
        }
    }
}
