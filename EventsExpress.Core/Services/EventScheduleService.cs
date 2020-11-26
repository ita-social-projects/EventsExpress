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
    public class EventScheduleService : IEventScheduleService
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public EventScheduleService(
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
            var eventScheduleDTO = EventScheduleByEventId(eventId);
            eventScheduleDTO.IsActive = false;
            return await Edit(eventScheduleDTO);
        }

        public async Task<OperationResult> CancelNextEvent(Guid eventId)
        {
            var eventScheduleDTO = EventScheduleByEventId(eventId);
            eventScheduleDTO.LastRun = eventScheduleDTO.NextRun;
            eventScheduleDTO.NextRun = DateTimeExtensions
                .AddDateUnit(eventScheduleDTO.Periodicity, eventScheduleDTO.Frequency, eventScheduleDTO.LastRun);
            return await Edit(eventScheduleDTO);
        }

        public async Task<OperationResult> Create(EventScheduleDTO eventScheduleDTO)
        {
            var ev = _mapper.Map<EventScheduleDTO, EventSchedule>(eventScheduleDTO);
            ev.CreatedBy = eventScheduleDTO.CreatedBy;
            ev.ModifiedBy = eventScheduleDTO.CreatedBy;
            ev.ModifiedDateTime = DateTime.Now;

            try
            {
                var result = _db.EventScheduleRepository.Insert(ev);
                await _db.SaveAsync();

                return new OperationResult(true, "Create new EventSchedule", result.Id.ToString());
            }
            catch (Exception ex)
            {
                return new OperationResult(false, ex.Message, string.Empty);
            }
        }

        public async Task<OperationResult> Edit(EventScheduleDTO eventScheduleDTO)
        {
            var ev = _db.EventScheduleRepository.Get(eventScheduleDTO.Id);
            ev.Frequency = eventScheduleDTO.Frequency;
            ev.Periodicity = eventScheduleDTO.Periodicity;
            ev.LastRun = eventScheduleDTO.LastRun;
            ev.NextRun = eventScheduleDTO.NextRun;
            ev.IsActive = eventScheduleDTO.IsActive;
            ev.EventId = eventScheduleDTO.EventId;
            ev.ModifiedBy = eventScheduleDTO.ModifiedBy;
            ev.ModifiedDateTime = DateTime.UtcNow;

            await _db.SaveAsync();
            return new OperationResult(true, "Edit event schedule", eventScheduleDTO.Id.ToString());
        }

                .Get("Event.Owners.User,Event.Photo")
                .FirstOrDefault(x => x.Id == id);

            return _mapper.Map<EventSchedule, EventScheduleDTO>(res);
        }

        public IEnumerable<EventScheduleDTO> GetAll()
        {
            var eventSchedules = _db.EventScheduleRepository
                .Get("Event.City.Country,Event.Photo,Event.Categories.Category")
                .Where(opt => opt.IsActive)
                .ToList();

            return _mapper.Map<IEnumerable<EventScheduleDTO>>(eventSchedules);
        }

        public IEnumerable<EventScheduleDTO> GetUrgentEventSchedules()
        {
            var eventSchedules = _db.EventScheduleRepository
                .Get()
                .Where(x => x.LastRun == DateTime.Today && x.IsActive == true)
                .ToList();

            return _mapper.Map<IEnumerable<EventScheduleDTO>>(eventSchedules);
        }

        public EventScheduleDTO EventScheduleByEventId(Guid eventId) =>
            _mapper.Map<EventSchedule, EventScheduleDTO>(_db.EventScheduleRepository
                .Get()
                .FirstOrDefault(x => x.EventId == eventId));
    }
}
