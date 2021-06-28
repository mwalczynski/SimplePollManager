namespace SimplePollManager.Api.Infrastructure.Configurations
{
    using System;
    using Microsoft.Extensions.Configuration;
    using Serilog;
    using Serilog.Core;

    public static class SerilogConfigurator
    {
        public static Logger CreateLogger()
        {
            var configuration = LoadAppConfiguration();
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            return logger;
        }

        private static IConfigurationRoot LoadAppConfiguration()
        {
            var configurationRoot = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .Build();

            return configurationRoot;
        }
    }
}
