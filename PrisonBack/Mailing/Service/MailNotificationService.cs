using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using PrisonBack.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PrisonBack.Mailing.Service
{
    public class MailNotificationService : IMailNotificationService
    {
        private readonly MailSettings _mailSettings;
        private readonly INotificationMail _notificationMail;
        private readonly MailRequest _mailRequest = new MailRequest();
        private readonly INotificationRepository _notificationRepository;


        public MailNotificationService(IOptions<MailSettings> mailSettings, INotificationMail notificationMail, INotificationRepository notificationRepository)
        {
            _mailSettings = mailSettings.Value;
            _notificationMail = notificationMail;
            _notificationRepository = notificationRepository;
        }

         public async Task SendEmailAsync()
        {
            foreach (var item in _notificationRepository.EmailList())
            {
                
                _mailRequest.Body = _notificationMail.Body(_notificationRepository.UserName(item));
                _mailRequest.Subject = _notificationMail.Title();
                _mailRequest.ToEmail = _notificationMail.To(item);

                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
                email.To.Add(MailboxAddress.Parse(_mailRequest.ToEmail));
                email.Subject = _mailRequest.Subject;
                var builder = new BodyBuilder();
                builder.HtmlBody = _mailRequest.Body;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
            
        }

        
    }
}
