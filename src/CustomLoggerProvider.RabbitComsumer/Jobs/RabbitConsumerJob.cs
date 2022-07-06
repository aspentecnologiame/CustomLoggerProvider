using CustomLoggerProvider.Domain.Interfaces.Services;
using CustomLoggerProvider.RabbitComsumer.Jobs.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLoggerProvider.RabbitComsumer.Jobs
{
    public class RabbitConsumerJob : IRabbitConsumerJob
    {
        private readonly IRabbitMQService _rabbitMQService;

        public RabbitConsumerJob(IRabbitMQService rabbitMQService)
        {
            _rabbitMQService = rabbitMQService;
        }

        public async Task SatrtConsumeQueue()
        {
            await _rabbitMQService.SatrtConsumeQueue();
        }
    }
}
