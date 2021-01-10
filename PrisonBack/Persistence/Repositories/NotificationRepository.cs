using Microsoft.EntityFrameworkCore;
using PrisonBack.Domain.Models;
using PrisonBack.Domain.Repositories;
using PrisonBack.Persistence.Context;
using PrisonBack.Resources.ViewModels;
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
        public List<Prisoner> ListOfPrisoner(int prisonId)
        {
            var prisoner = _context.Prisoners.Include(x => x.Punishments).Where(x => x.Cell.IdPrison == prisonId);
            List<Prisoner> prisoners = new List<Prisoner>();
            foreach (var item in prisoner)
            {
                var prisonerNearEnd = item.Punishments.FirstOrDefault(x => x.EndDate <= DateTime.Now.AddDays(1));


                if (prisonerNearEnd != null)
                { 
                    prisoners.Add(item);

                }
                
            }


            return prisoners;
        }
        public UserPermission UserPermission(string userName)
        {
            var user = _context.UserPermissions.FirstOrDefault(x => x.UserName == userName);
            return user;
        }
        public List<string> EmailList()
        {
            List<string> emails = new List<string>();

            var user = _context.Users;
            foreach (var item in user)
            {
                emails.Add(item.Email);
            }

            return emails;
            }
        public string UserName (string email){

            var user = _context.Users.FirstOrDefault(x => x.Email == email);
            string userName = user.UserName;
            return userName;

        }
        }
     
     }

