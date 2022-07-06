using CustomLoggerProvider.Domain.Interfaces.Services;
using CustomLoggerProvider.Infra.Rabbit;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using CustomLoggerProvider.Infra.Sql;
using CustomLoggerProvider.Domain.Models;

namespace CustomLoggerProvider.Services
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly ILogger<RabbitMQService> _logger;
        private readonly RabbitMQRepository _rabbitMQRepository;
        private readonly SqlServerRepository _sqlServerRepository;

        public RabbitMQService(ILogger<RabbitMQService> logger, 
            RabbitMQRepository rabbitMQRepository, SqlServerRepository sqlServerRepository)
        {
            _logger = logger;   
            _rabbitMQRepository = rabbitMQRepository;
            _sqlServerRepository = sqlServerRepository;
        }

        public async Task SatrtConsumeQueue()
        {
            _rabbitMQRepository.Subscribe(message => this.HanldeMessage(message));
            await Task.FromResult(Task.CompletedTask);
        }

        public void HanldeMessage(string message)
        {
            _logger.LogInformation(message);
            var eventLog = JsonSerializer.Deserialize<EventLog>(message);
            _sqlServerRepository.Add(eventLog);
        }
    }
}
