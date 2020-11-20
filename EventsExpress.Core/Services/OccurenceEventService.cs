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

        public async Task<OperationResult> CancelEvents(Guid eventId)
        {
            var eventDTO = OccurenceEventByEventId(eventId);
            eventDTO.IsActive = false;
            return await Edit(eventDTO);
        }

        public async Task<OperationResult> CancelNextEvent(Guid eventId)
        {
            var eventDTO = OccurenceEventByEventId(eventId);
            eventDTO.LastRun = eventDTO.NextRun;
            eventDTO.NextRun = DateTimeExtensions
                .AddDateUnit(eventDTO.Periodicity, eventDTO.Frequency, eventDTO.LastRun);
            return await Edit(eventDTO);
        }

        public async Task<OperationResult> Create(OccurenceEventDTO eventDTO)
        {
            var ev = _mapper.Map<OccurenceEventDTO, OccurenceEvent>(eventDTO);
            ev.CreatedBy = eventDTO.Event.OwnerId;
            ev.CreatedDateTime = DateTime.UtcNow;

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
            ev.CreatedDateTime = DateTime.UtcNow;
            ev.ModifiedBy = eventDTO.ModifiedBy;
            ev.ModifiedDateTime = DateTime.UtcNow;

            await _db.SaveAsync();
            return new OperationResult(true, "Edit occurence event", ev.Id.ToString());
        }

        public OccurenceEventDTO OccurenceEventById(Guid Id) =>
            _mapper.Map<OccurenceEventDTO>(_db.OccurenceEventRepository
                .Get("Event.City.Country,Event.Photo,Event.Categories.Category")
                .FirstOrDefault(x => x.Id == Id));

        public IEnumerable<OccurenceEventDTO> GetAll()
        {
            var events = _db.OccurenceEventRepository
                .Get("Event.City.Country,Event.Photo,Event.Owner,Event.Categories.Category")
                .Where(opt => opt.IsActive == true)
                .ToList();

            return _mapper.Map<IEnumerable<OccurenceEventDTO>>(events);
        }

        public IEnumerable<OccurenceEventDTO> GetUrgentOccurenceEvents()
        {
            var events = _db.OccurenceEventRepository
                .Get()
                .Where(x => x.LastRun == DateTime.Today && x.IsActive == true)
                .ToList();

            return _mapper.Map<IEnumerable<OccurenceEventDTO>>(events);
        }

        public OccurenceEventDTO OccurenceEventByEventId(Guid eventId) =>
            _mapper.Map<OccurenceEventDTO>(_db.OccurenceEventRepository
                .Get()
                .FirstOrDefault(x => x.EventId == eventId));
    }
}
