using CustomLoggerProvider.Domain.Interfaces.Services;
using CustomLoggerProvider.Domain.Models;
using CustomLoggerProvider.Infra.Sql;

namespace CustomLoggerProvider.Services
{
    public class SqlServerService : ISqlServerService
    {
        private readonly SqlServerRepository _sqlServerRepository;
        public SqlServerService(SqlServerRepository sqlServerRepository)
        {
            _sqlServerRepository = sqlServerRepository;
        }

        public void Add(EventLog eventLog)
        {
            _sqlServerRepository.Add(eventLog);
        }
    }
}
