namespace SimplePollManager.Api
{
    using HealthChecks.UI.Client;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics.HealthChecks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using SimplePollManager.Api.Filters;
    using SimplePollManager.Api.Services;
    using SimplePollManager.Application.Common.Extensions;
    using SimplePollManager.Application.Common.Interfaces;
    using SimplePollManager.Domain.Extensions;
    using SimplePollManager.Infrastructure.Extensions;

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
                    options.Filters.Add<ApiExceptionFilter>();
                    options.Filters.Add<ValidateModelStateFilter>();
                })
                .AddApiExplorer()
                .AddDataAnnotations()
                .SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddSwaggerGen();

            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddDomainComponents();
            services.AddApplicationComponents();
            services.AddInfrastructureComponents(this.configuration);
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
