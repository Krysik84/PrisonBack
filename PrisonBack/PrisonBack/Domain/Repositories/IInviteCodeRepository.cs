using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Domain.Repositories
{
    public interface IInviteCodeRepository
    {
        bool IsActive(string code);
        void ChangeStatus(string code);
        string CreateCode();
    }
}
