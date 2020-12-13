using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Domain.Repositories
{
    public interface IAddUserRepository
    {
        void AddUserToPrison(string code, string userName);
    }
}
