using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using PrisonBack.Domain.Repositories;
using PrisonBack.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PrisonBack.Mailing.Service
{
    public class MailNotificationService : IHostedService
    {
        private readonly MailSettings _mailSettings;
        private readonly NotificationMail _registerMail;
        private readonly MailRequest _mailRequest = new MailRequest();

        public MailNotificationService(IOptions<MailSettings> mailSettings, NotificationMail registerMail)
        {
            _mailSettings = mailSettings.Value;
            _registerMail = registerMail;
        }

         public async Task SendEmailAsync()
        {
            _mailRequest.Body = _registerMail.Body();
            _mailRequest.Subject = _registerMail.Title();
            _mailRequest.ToEmail = _registerMail.To();

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

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(TaskRoutine, cancellationToken);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Sync Task stopped");
            return null;
        }
        public Task TaskRoutine()
        {


            while (true)
            {

                SendEmailAsync();
                DateTime nextStop = DateTime.Now.AddDays(1);
                var timeToWait = nextStop - DateTime.Now;
                var millisToWait = timeToWait.TotalMilliseconds;
                Thread.Sleep((int)millisToWait);
            }
        }
    }
}
