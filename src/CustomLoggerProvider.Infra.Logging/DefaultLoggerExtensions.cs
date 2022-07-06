using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using CustomLoggerProvider.Domain.Options;
using System;
using CustomLoggerProvider.Infra.Rabbit;
using CustomLoggerProvider.Infra.Sql;
using CustomLoggerProvider.Domain.Interfaces.Repositories;

namespace CustomLoggerProvider.Infra.Logging
{
    public static class DefaultLoggerExtensions
    {
        public static ILoggingBuilder AddDefaultLoggerProvider(this ILoggingBuilder builder, Action<DefaultLoggerOptions> options)
        {
            builder.Services.AddSingleton<ILoggerProvider, DefaultLoggerProvider>();
            builder.Services.Configure(options);
            return builder;
        }

        public static IServiceCollection AddRabbitMQLoggerService(this IServiceCollection service)
        {
            service.AddSingleton<IDefaultLoggerRepository, RabbitMQRepository>();
            return service;
        }

        public static IServiceCollection AddSQLServerLoggerService(this IServiceCollection service)
        {
            service.AddSingleton<IDefaultLoggerRepository, SqlServerRepository>();
            return service;
        }
    }
}
