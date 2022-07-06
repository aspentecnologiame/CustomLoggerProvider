using CustomLoggerProvider.Infra.SqlServer.Models;
using CustomLoggerProvider.Infra.SqlServer.Repositories;
using Microsoft.Extensions.Configuration;

namespace CustomLoggerProvider.Infra.SqlServer.Logging
{
    public abstract class LoggerRepository : AbstractRepository<EventLog>
    {
        public LoggerRepository(IConfiguration configuration) : base(configuration){}
    }
}