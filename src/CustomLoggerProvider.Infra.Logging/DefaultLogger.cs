using CustomLoggerProvider.Domain.Models;
using CustomLoggerProvider.Domain.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using CustomLoggerProvider.Domain.Interfaces.Repositories;

namespace CustomLoggerProvider.Infra.Logging
{
    public class DefaultLogger : ILogger
    {
        private string _categoryName;
        private readonly IOptions<DefaultLoggerOptions> _options;
        private readonly IDefaultLoggerRepository _defaultLoggerRepository;
        private IExternalScopeProvider ScopeProvider { get; set; }
        private readonly int maxLength = 1024;
        public DefaultLogger(string categoryName, IOptions<DefaultLoggerOptions> options,
            IDefaultLoggerRepository defaultLoggerRepository)
        {
            _categoryName = categoryName;
            _options = options;
            _defaultLoggerRepository = defaultLoggerRepository;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            var logBuilder = new StringBuilder();

            if (!IsEnabled(logLevel)) return;

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }
            var message = formatter(state, exception);
            if (!string.IsNullOrEmpty(message))
            {
                logBuilder.Append(message);
                logBuilder.Append(Environment.NewLine);
            }

            GetScope(logBuilder);

            if (exception != null)
                logBuilder.Append(exception.ToString());

            if (logBuilder.Capacity > maxLength)
                logBuilder.Capacity = maxLength;

            var eventLog = new EventLog
            {
                Message = message,
                EventId = eventId.Id,
                Category = _categoryName,
                LogLevel = logLevel.ToString(),
                CreatedTime = DateTime.UtcNow
            };

            _defaultLoggerRepository.Add(eventLog); 
        }

        private void GetScope(StringBuilder stringBuilder)
        {
            var scopeProvider = ScopeProvider;
            if (scopeProvider != null)
            {
                var initialLength = stringBuilder.Length;

                scopeProvider.ForEachScope((scope, state) =>
                {
                    var (builder, length) = state;
                    var first = length == builder.Length;
                    builder.Append(first ? "=> " : " => ").Append(scope);
                }, (stringBuilder, initialLength));

                stringBuilder.AppendLine();
            }
        }
    }
}
