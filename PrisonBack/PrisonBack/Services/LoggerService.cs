using PrisonBack.Domain.Repositories;
using PrisonBack.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Services
{
    public class LoggerService : ILoggerService
    {
        private readonly ILoggerRepository _loggerRepository;
        public LoggerService(ILoggerRepository loggerRepository)
        {
            _loggerRepository = loggerRepository;
        }
        public void AddLog(string controller, string action, int id)
        {
            _loggerRepository.AddLog(controller, action, id);
        }
    }
}
