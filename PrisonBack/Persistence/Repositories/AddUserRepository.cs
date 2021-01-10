using PrisonBack.Domain.Models;
using PrisonBack.Domain.Repositories;
using PrisonBack.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Persistence.Repositories
{
    public class AddUserRepository : BaseRepository, IAddUserRepository
    {
        UserPermission userPermission = new UserPermission();
        public AddUserRepository(AppDbContext context) : base(context)
        {

        }

        public void AddUserToPrison(string code, string userName)
        {
            var permission = _context.InviteCodes.FirstOrDefault(x => x.Code == code);
            if(permission != null)
            {
                userPermission.IdPrison = permission.IdPrison;
                userPermission.UserName = userName;
                if(userPermission != null)
                {
                    _context.UserPermissions.Add(userPermission);
                    _context.SaveChanges();
                }
            }
            
        }
    }
}
