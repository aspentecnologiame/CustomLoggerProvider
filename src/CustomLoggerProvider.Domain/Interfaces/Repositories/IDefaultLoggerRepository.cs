using CustomLoggerProvider.Domain.Models;

namespace CustomLoggerProvider.Domain.Interfaces.Repositories
{
    public interface IDefaultLoggerRepository
    {
        void Add(EventLog eventLog);
    }
}
