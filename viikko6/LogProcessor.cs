using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vk2.Models;

namespace vk2.Processors
{
    public class LogProcessor
    {
        private IRepository _repository;

        public LogProcessor(IRepository repository)
        {
            _repository = repository;
        }

        public Task<LogEntry[]> GetLog()
        {
            return _repository.GetLog();
        }
}
}