using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLoggerProvider.Domain.Interfaces.Services
{
    public interface IRabbitMQService
    {
        Task SatrtConsumeQueue();
    }
}
