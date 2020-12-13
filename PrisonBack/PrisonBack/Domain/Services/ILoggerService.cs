using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Domain.Services
{
    public interface ILoggerService
    {
        void AddLog(string controller, string action, string userName);
    }
}
