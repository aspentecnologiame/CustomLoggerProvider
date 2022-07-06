using CustomLoggerProvider.RabbitComsumer.DI;
using CustomLoggerProvider.RabbitComsumer.Jobs.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CustomLoggerProvider.RabbitComsumer.WorkerService
{
    public class Worker : BackgroundService
    {
        private static IConfiguration Configuration { get; set; }
        public static ServiceProvider ServiceProvider { get; set; }
        private readonly ILogger<Worker> _logger;
        private readonly IRabbitConsumerJob _rabbitConsumerJob;

        public Worker(ILogger<Worker> logger, IRabbitConsumerJob rabbitConsumerJob, IConfiguration configuration)
        {
            _logger = logger;
            _rabbitConsumerJob = rabbitConsumerJob;
            Configuration = configuration;
        }

        public static void ConfigureService(IServiceCollection serviceCollection)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location))
                .AddJsonFile("appsettings.json")
                .Build();

            serviceCollection.AddLogging(loggingBuilder => loggingBuilder.SetMinimumLevel(LogLevel.Trace));

            DIBootstrap.Configure(serviceCollection, Configuration);
            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _rabbitConsumerJob.SatrtConsumeQueue();
            while(!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(5000, stoppingToken);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Parando o Rabbit Consumer");
            await base.StopAsync(cancellationToken);
        }
    }
}
