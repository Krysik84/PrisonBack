using PrisonBack.Domain.Repositories;
using PrisonBack.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrisonBack.Mailing
{
    public class NotificationMail : INotificationMail
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationMail(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
       
        public string Body(string userName)
        {
            string test = "Lista więźniów którzy wychodzą w ciągu najbliższych dni: <br /> ";
            //var user = _notificationRepository.UserPermission(userName);
            foreach (var item in _notificationRepository.ListOfPrisoner(1))
            {
                
                test += item.Forname + " ";
                test += item.Name + " <br /> ";

            }
            {

            }
            return test;//"PrisonBreak <br /> Dzisiaj jest " + DateTime.Now+ "<br /> Życzymi miłej pracy <br /> Team PrisonBreak";
        }
        public string Title()
        {
            return "Notyfikacja";
        }
        public string To(string email)
        {
            return email;
        }
    }
}
