using PrisonBack.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Domain.Repositories
{
    public interface INotificationRepository
    {
         List<Prisoner> ListOfPrisoner(int prisonId);
        UserPermission UserPermission(string userName);
        List<string> EmailList();
        string UserName(string email);

    }
}
