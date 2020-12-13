using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Domain.Repositories
{
    public interface ILoggerRepository
    {
        bool SaveChanges();
        void AddLog(string controller, string action, string userName);
    }
}
