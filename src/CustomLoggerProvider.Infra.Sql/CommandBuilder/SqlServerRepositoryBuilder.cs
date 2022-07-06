using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLoggerProvider.Infra.Sql.CommandBuilder
{
    public class SqlServerRepositoryBuilder
    {
        public const string Insert = @"INSERT INTO EventLog (Category, EventId, LogLevel, Message, CreatedTime) 
                VALUES(@Category, @EventId, @LogLevel, @Message, @CreatedTime)";
    }
}
