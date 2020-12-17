using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PrisonBack.Domain.Services;
using PrisonBack.Mailing.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PrisonBack.Mailing
{
    public class TimerService : IHostedService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public TimerService( IServiceScopeFactory serviceScopeFactory)
        {

            _serviceScopeFactory = serviceScopeFactory;
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
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var myScopedService = scope.ServiceProvider.GetService<IMailNotificationService>();

                    myScopedService.SendEmailAsync();
                    DateTime nextStop = DateTime.Now.AddDays(1);
                    var timeToWait = nextStop - DateTime.Now;
                    var millisToWait = timeToWait.TotalMilliseconds;
                    Thread.Sleep((int)millisToWait);
                }

                
            }
        }
    }
}
