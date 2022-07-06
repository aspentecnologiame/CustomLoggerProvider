using System.Data;
using System.Data.SqlClient;
using System.IO;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace CustomLoggerProvider.Infra.SqlServer.Db
{
    public class Seed
    {
        private static IDbConnection _dbConnection;

        public static void CreateDb(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("CustomLoggerProvider");

            _dbConnection = new SqlConnection(connectionString);
            _dbConnection.Open();

            // Create a Product table
            _dbConnection.Execute(@"
                    IF OBJECT_ID(N'[dbo].[EventLog]', N'U') IS NULL
                        BEGIN
                            CREATE TABLE [EventLog] (
                                [Id] INT NOT NULL PRIMARY KEY IDENTITY (1,1),
                                [Category] NVARCHAR(512) NOT NULL,
                                [EventId] INT NULL,
                                [LogLevel] NVARCHAR(32) NOT NULL,
                                [Message] NVARCHAR(1024) NOT NULL,
                                [CreatedTime] DATETIME NOT NULL
                            )
                        END
                ");

            _dbConnection.Close();
        }
    }
}