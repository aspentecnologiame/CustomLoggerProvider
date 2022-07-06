using CustomLoggerProvider.Domain.Interfaces.Repositories;
using CustomLoggerProvider.Domain.Interfaces.Services;
using CustomLoggerProvider.Domain.Options;
using CustomLoggerProvider.Infra.Rabbit;
using CustomLoggerProvider.Infra.Sql;
using CustomLoggerProvider.RabbitComsumer.Jobs;
using CustomLoggerProvider.RabbitComsumer.Jobs.Interfaces;
using CustomLoggerProvider.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLoggerProvider.RabbitComsumer.DI
{
    public static class DIBootstrap
    {
        public static void Configure(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IRabbitMQService, RabbitMQService>();
            services.AddSingleton<ISqlServerService, SqlServerService>();
            services.AddSingleton<RabbitMQRepository>();
            services.AddSingleton<SqlServerRepository>(provider => new SqlServerRepository(configuration.GetConnectionString("CustomLoggerProvider")));
            services.AddSingleton<IRabbitConsumerJob, RabbitConsumerJob>();
            services.Configure<DefaultLoggerOptions>(options =>
            {
                configuration.GetSection("DefaultLoggerOptions").Bind(options);
            });
        }
    }
}
