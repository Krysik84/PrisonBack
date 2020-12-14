using PrisonBack.Domain.Repositories;
using PrisonBack.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Services
{
    public class InviteCodeService : IInviteCodeService
    {
        private readonly IInviteCodeRepository _inviteCodeRepository;

        public InviteCodeService(IInviteCodeRepository inviteCodeRepository)
        {
            _inviteCodeRepository = inviteCodeRepository;
        }

        public void ChangeStatus(string code)
        {
            _inviteCodeRepository.ChangeStatus(code);
        }

        public string CreateCode()
        {
            return _inviteCodeRepository.CreateCode();
        }

        public bool IsActive(string code)
        {
            return _inviteCodeRepository.IsActive(code);
        }
    }
}
