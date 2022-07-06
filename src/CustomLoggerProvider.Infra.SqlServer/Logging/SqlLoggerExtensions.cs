using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CustomLoggerProvider.Infra.SqlServer.Logging
{
    public static class SqlLoggerExtensions 
    {
        public static ILoggingBuilder AddSqlProvider<T>(this ILoggingBuilder builder, T repository) where T: LoggerRepository
        {
            builder.Services.AddSingleton<ILoggerProvider, SqlLoggerProvider<T>>(p => new SqlLoggerProvider<T>((_, logLevel) => logLevel >= LogLevel.Debug, repository));

            return builder;
        }
    }
}