
namespace CustomLoggerProvider.Domain.Options
{
    public class RabbitMQOptions
    {
        public string HostName { get; set; }
        public string Exchange { get; set; }
        public string QueueName { get; set; }
        public string RoutingKey { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
