using PrisonBack.Domain.Models;
using PrisonBack.Domain.Repositories;
using PrisonBack.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Persistence.Repositories
{
    public class InviteCodeRepository : BaseRepository, IInviteCodeRepository
    {
        InviteCode inviteCode = new InviteCode();

        public InviteCodeRepository(AppDbContext context) : base(context)
        {
        }

        public void ChangeStatus(string code)
        {
            var inviteCode = _context.InviteCodes.FirstOrDefault(x => x.Code == code);
            inviteCode.Status = false;
            _context.SaveChanges();
        }

        public string CreateCode()
        {
            string guid = Guid.NewGuid().ToString();
            inviteCode.Code = guid;
            inviteCode.Status = true;
            inviteCode.IdPrison = 1;
            _context.Add(inviteCode);
            _context.SaveChanges();
            return guid;
        }

        public bool IsActive(string code)
        {
            var exist =_context.InviteCodes.FirstOrDefault(x => x.Code == code);
            if ((exist == null)||(exist.Status == false))
            {
                return false;
            }
            return true;
        }
    }
}

