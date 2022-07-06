using CustomLoggerProvider.Domain.Models;
using Microsoft.Extensions.Options;
using System;
using CustomLoggerProvider.Domain.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using CustomLoggerProvider.Domain.Interfaces.Repositories;
using RabbitMQ.Client.Events;

namespace CustomLoggerProvider.Infra.Rabbit
{
    public class RabbitMQRepository : IDefaultLoggerRepository
    {
        private readonly IOptions<DefaultLoggerOptions> _options;
        public RabbitMQRepository(IOptions<DefaultLoggerOptions> options)
        {
            _options = options;
        }
        public void Add(EventLog eventLog)
        {
            var factory = CreateConnectionFactory();

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var message = JsonSerializer.Serialize(eventLog);
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish(exchange: _options.Value.RabbitMQOptions.Exchange,
                                     routingKey: _options.Value.RabbitMQOptions.RoutingKey,
                                     basicProperties: null,
                                     body: body);
            }
        }

        public void Subscribe(Action<string> callBack)
        {
            var factory = CreateConnectionFactory();
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueBind(queue: _options.Value.RabbitMQOptions.QueueName,
                                  exchange: _options.Value.RabbitMQOptions.Exchange,
                                  routingKey: _options.Value.RabbitMQOptions.RoutingKey);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                callBack(message);
            };

            channel.BasicConsume(queue: _options.Value.RabbitMQOptions.QueueName,
                                 autoAck: true,
                                 consumer: consumer);
        }
        
        private ConnectionFactory CreateConnectionFactory()
        {
            return new ConnectionFactory
            {
                HostName = _options.Value.RabbitMQOptions.HostName,
                UserName = _options.Value.RabbitMQOptions.UserName,
                Password = _options.Value.RabbitMQOptions.Password
            };
        }
    }
}
