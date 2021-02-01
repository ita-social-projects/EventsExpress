using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EventsExpress.Core.HostedService
{
    public class SendMessageHostedService : BackgroundService
    {
        private readonly ILogger<SendMessageHostedService> _logger;
        private readonly IMapper _mapper;

        public SendMessageHostedService(
            IServiceProvider services,
            ILogger<SendMessageHostedService> logger,
            IMapper mapper)
        {
            Services = services;
            _logger = logger;
            _mapper = mapper;
        }

        public IServiceProvider Services { get; }

        private async Task DoWork(CancellationToken stoppingToken)
        {
            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService =
                    scope.ServiceProvider
                        .GetRequiredService<IEventScheduleService>();

                var mediator =
                    scope.ServiceProvider
                        .GetRequiredService<IMediator>();
                while (!stoppingToken.IsCancellationRequested)
                {
                    var events = scopedProcessingService.GetUrgentEventSchedules();
                    try
                    {
                        foreach (var ev in events)
                        {
                            await mediator.Publish(new CreateEventVerificationMessage(_mapper.Map<EventScheduleDto>(ev)));
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex.Message);
                    }

                    await Task.Delay(1000 * 60 * 60 * 24, stoppingToken);

                    _logger.LogInformation("Message Hosted Service is working.");
                }
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation(
            "Message Service Hosted Service is stopping.");

            await base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
            "Message Service Hosted Service running.");

            await DoWork(stoppingToken);
        }
    }
}
