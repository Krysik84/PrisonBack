using PrisonBack.Domain.Models;
using PrisonBack.Domain.Repositories;
using PrisonBack.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Persistence.Repositories
{
    public class NotificationRepository : BaseRepository, INotificationRepository
    {
        public NotificationRepository(AppDbContext context) : base(context)
        {

        }
        public List<Prisoner> ListOfPrisoner(string userName)
        {
            var prison = _context.UserPermissions.FirstOrDefault(x => x.UserName == userName);
            var prisoner = _context.Prisoners.Where(x => x.Cell.IdPrison == prison.IdPrison);
            List<Prisoner> prisoners = new List<Prisoner>();
            foreach (var item in prisoner)
            {
                var punishment = _context.Punishments.FirstOrDefault(x => x.IdPrisoner == item.Id);
                if (punishment.EndDate <= DateTime.Now.AddDays(2))
                {
                    prisoners.Add(item);
                }
            }


            return prisoners;
        }
     }
}
