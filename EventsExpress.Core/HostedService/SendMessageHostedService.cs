using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventsExpress.Core.IServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EventsExpress.Core.HostedService
{
    public class SendMessageHostedService : BackgroundService
    {
        private readonly ILogger<SendMessageHostedService> _logger;

        public SendMessageHostedService(
            IServiceProvider services,
            ILogger<SendMessageHostedService> logger)
        {
            Services = services;
            _logger = logger;
        }

        public IServiceProvider Services { get; }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<IOccurenceEventService>();

                await scopedProcessingService.EventNotification(stoppingToken);
            }

            _logger.LogInformation(
                "Message Hosted Service is working.");
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
            "Message Service Hosted Service is stopping.");

            await base.StopAsync(stoppingToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
            "Message Service Hosted Service running.");

            await DoWork(stoppingToken);
        }
    }
}
