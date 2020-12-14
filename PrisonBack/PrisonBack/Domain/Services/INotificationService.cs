using PrisonBack.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Domain.Services
{
    public interface INotificationService
    {
        List<Prisoner> ListOfPrisoner(string userName);

    }
}
