using System;
using Dapper;
using System.Data.SqlClient;
using CustomLoggerProvider.Domain.Models;
using CustomLoggerProvider.Domain.Interfaces.Repositories;
using CustomLoggerProvider.Infra.Sql.CommandBuilder;

namespace CustomLoggerProvider.Infra.Sql
{
    public class SqlServerRepository : IDefaultLoggerRepository
    {
        private readonly string _connectionString;
        public SqlServerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public async void Add(EventLog eventLog)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync(SqlServerRepositoryBuilder.Insert, eventLog);
        }
    }
}
