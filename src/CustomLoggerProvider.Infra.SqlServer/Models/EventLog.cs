using System;

namespace CustomLoggerProvider.Infra.SqlServer.Models
{
    public class EventLog
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public int EventId { get; set; }
        public string LogLevel { get; set; }
        public string Message { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}