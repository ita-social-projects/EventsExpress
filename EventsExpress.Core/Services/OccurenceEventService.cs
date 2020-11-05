using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;
using MediatR;

namespace EventsExpress.Core.Services
{
    public class OccurenceEventService : IOccurenceEventService
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public OccurenceEventService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IMediator mediator)
        {
            _db = unitOfWork;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<OperationResult> Create(OccurenceEventDTO eventDTO)
        {
            var ev = _mapper.Map<OccurenceEventDTO, OccurenceEvent>(eventDTO);

            try
            {
                var result = _db.OccurenceEventRepository.Insert(ev);
                await _db.SaveAsync();

                return new OperationResult(true, "Create new OccurenceEvent", result.Id.ToString());
            }
            catch (Exception ex)
            {
                return new OperationResult(false, ex.Message, string.Empty);
            }
        }

        public async Task<OperationResult> Edit(OccurenceEventDTO eventDTO)
        {
            var ev = _db.OccurenceEventRepository.Get().FirstOrDefault(x => x.Id == eventDTO.Id);
            ev.Frequency = eventDTO.Frequency;
            ev.Periodicity = eventDTO.Periodicity;
            ev.LastRun = eventDTO.LastRun;
            ev.NextRun = eventDTO.NextRun;
            ev.IsActive = eventDTO.IsActive;
            ev.EventId = eventDTO.EventId;
            ev.CreatedBy = eventDTO.CreatedBy;
            ev.CreatedDate = eventDTO.CreatedDate;
            ev.ModifiedBy = eventDTO.ModifiedBy;
            ev.ModifiedDate = eventDTO.ModifiedDate;

            await _db.SaveAsync();
            return new OperationResult(true, "Edit occurence event", ev.Id.ToString());
        }

        public OccurenceEventDTO EventById(Guid eventId) =>
            _mapper.Map<OccurenceEventDTO>(_db.OccurenceEventRepository
                .Get()
                .FirstOrDefault(x => x.Id == eventId));

        public async Task EventNotification(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var events = _db.OccurenceEventRepository.Get().Where(x => x.IsActive == false && x.LastRun == DateTime.Today);

                try
                {
                    foreach (var ev in events)
                    {
                        await _mediator.Publish(new CreateEventVerificationMessage(_mapper.Map<OccurenceEventDTO>(ev)));
                    }
                }
                catch (Exception ex)
                {
                   new OperationResult(false, ex.Message, string.Empty);
                }

                await Task.Delay(1000 * 60 * 60 * 24, stoppingToken);
            }
        }
    }
}
