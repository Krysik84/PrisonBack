using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Domain.Services
{
    public interface IInviteCodeService
    {
        bool IsActive(string code);
        void ChangeStatus(string code);
        string CreateCode(string userName);
    }
}
