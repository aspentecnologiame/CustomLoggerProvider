using CustomLoggerProvider.Domain.Interfaces.Repositories;
using CustomLoggerProvider.Domain.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace CustomLoggerProvider.Infra.Logging
{
    public class DefaultLoggerProvider : ILoggerProvider
    {
        private readonly IOptions<DefaultLoggerOptions> _options;
        private readonly IDefaultLoggerRepository _defaultCustomLoggerProviderRepository;
        public DefaultLoggerProvider(IOptions<DefaultLoggerOptions> options,
            IDefaultLoggerRepository defaultCustomLoggerProviderRepository)
        {
            _options = options;
            _defaultCustomLoggerProviderRepository = defaultCustomLoggerProviderRepository;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DefaultLogger(categoryName, _options, _defaultCustomLoggerProviderRepository);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
