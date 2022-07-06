using System;
using Microsoft.Extensions.Logging;

namespace CustomLoggerProvider.Infra.SqlServer.Logging
{
    public class SqlLoggerProvider<T>: ILoggerProvider where T : LoggerRepository
    {
        private readonly Func<string, LogLevel, bool> _filter;
        private readonly T _repository;

        public SqlLoggerProvider(Func<string, LogLevel, bool> filter, T repository)
        {
            this._filter = filter;
            this._repository = repository;
        }

        public ILogger CreateLogger(string categoryName) => new SqlLogger<T>(_filter, _repository, categoryName);

        public void Dispose() {}
    }
}