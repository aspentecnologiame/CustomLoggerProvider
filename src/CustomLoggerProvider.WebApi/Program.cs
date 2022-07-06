using CustomLoggerProvider.Infra.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace CustomLoggerProvider.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    //logging.AddSqlProvider(new EventLogRepository(hostingContext.Configuration));
                    logging.AddDefaultLoggerProvider(options => hostingContext.Configuration.GetSection("DefaultLoggerOptions").Bind(options));
                    logging.Services.AddRabbitMQLoggerService();
                });
    }
}
