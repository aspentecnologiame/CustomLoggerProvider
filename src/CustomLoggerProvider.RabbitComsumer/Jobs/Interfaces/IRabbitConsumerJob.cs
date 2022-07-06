using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLoggerProvider.RabbitComsumer.Jobs.Interfaces
{
    public interface IRabbitConsumerJob
    {
        Task SatrtConsumeQueue();
    }
}
