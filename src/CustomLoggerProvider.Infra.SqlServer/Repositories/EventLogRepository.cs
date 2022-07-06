using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CustomLoggerProvider.Infra.SqlServer.Logging;
using CustomLoggerProvider.Infra.SqlServer.Models;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace CustomLoggerProvider.Infra.SqlServer.Repositories
{
    public class EventLogRepository : LoggerRepository
    {
        public EventLogRepository(IConfiguration configuration) : base(configuration){}

        public override void Add(EventLog item)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = "INSERT INTO EventLog (Category, EventId, LogLevel, Message, CreatedTime)"
                                + " VALUES(@Category, @EventId, @LogLevel, @Message, @CreatedTime)";
                dbConnection.Open();
                dbConnection.Execute(sQuery, item);
            }
        }

        public override IEnumerable<EventLog> FindAll()
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<EventLog>("SELECT * FROM EventLog");
            }
        }

        public override EventLog FindByID(int id)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = "SELECT * FROM EventLog" 
                            + " WHERE Id = @Id";
                dbConnection.Open();
                return dbConnection.Query<EventLog>(sQuery, new { Id = id }).FirstOrDefault();
            }
        }

        public override void Remove(int id)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = "DELETE FROM EventLog" 
                            + " WHERE Id = @Id";
                dbConnection.Open();
                dbConnection.Execute(sQuery, new { Id = id });
            }
        }

        public override void Update(EventLog item)
        {
            using (IDbConnection dbConnection = new SqlConnection(ConnectionString))
            {
                string sQuery = "UPDATE EventLog SET Category = @Category,"
                            + " EventId = @EventId, LogLevel= @LogLevel," 
                            + " Message = @Message, CreatedTime= @CreatedTime" 
                            + " WHERE Id = @Id";
                dbConnection.Open();
                dbConnection.Query(sQuery, item);
            }
        }
    }
}