using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Domain.Services
{
    public interface IAddUserService
    {
        void AddUserToPrison(string code, string userName);
    }
}
