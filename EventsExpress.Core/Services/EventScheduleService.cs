using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.BaseService;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services
{
    public class EventScheduleService : BaseService<EventSchedule>, IEventScheduleService
    {
        public EventScheduleService(AppDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public IEnumerable<EventScheduleDTO> GetAll()
        {
            var eventSchedules = _db.EventScheduleRepository
                .Get("Event.City.Country,Event.Photo,Event.Categories.Category")
                .Where(opt => opt.IsActive)
                .ToList();

            return _mapper.Map<IEnumerable<EventScheduleDTO>>(eventSchedules);
        }

        public EventScheduleDTO EventScheduleById(Guid eventScheduleId)
        {
            var res = _db.EventScheduleRepository
                .Get("Event.Owners.User,Event.Photo")
                .FirstOrDefault(x => x.Id == eventScheduleId);

            return _mapper.Map<EventSchedule, EventScheduleDTO>(res);
        }

        public EventScheduleDTO EventScheduleByEventId(Guid eventId) =>
            _mapper.Map<EventSchedule, EventScheduleDTO>(_db.EventScheduleRepository
                .Get()
                .FirstOrDefault(x => x.EventId == eventId));

        public IEnumerable<EventScheduleDTO> GetUrgentEventSchedules()
        {
            var eventSchedules = _db.EventScheduleRepository
                .Get()
                .Where(x => x.LastRun == DateTime.Today && x.IsActive == true)
                .ToList();

            return _mapper.Map<IEnumerable<EventScheduleDTO>>(eventSchedules);
        }

        public async Task<OperationResult> Create(EventScheduleDTO eventScheduleDTO)
        {
            var eventScheduleEntity = _mapper.Map<EventScheduleDTO, EventSchedule>(eventScheduleDTO);
            eventScheduleEntity.CreatedBy = eventScheduleDTO.CreatedBy;

            try
            {
                var result = Insert(eventScheduleEntity);
                await _context.SaveChangesAsync();

                return new OperationResult(true, "Create new EventSchedule", result.Id.ToString());
            }
            catch (Exception ex)
            {
                return new OperationResult(false, ex.Message, string.Empty);
            }
        }

        public async Task<OperationResult> Edit(EventScheduleDTO eventScheduleDTO)
        {
            var ev = _context.EventSchedules.Find(eventScheduleDTO.Id);
            ev.Frequency = eventScheduleDTO.Frequency;
            ev.Periodicity = eventScheduleDTO.Periodicity;
            ev.LastRun = eventScheduleDTO.LastRun;
            ev.NextRun = eventScheduleDTO.NextRun;
            ev.IsActive = eventScheduleDTO.IsActive;
            ev.EventId = eventScheduleDTO.EventId;
            ev.ModifiedBy = eventScheduleDTO.ModifiedBy;
            ev.ModifiedDateTime = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return new OperationResult(true, "Edit event schedule", eventScheduleDTO.Id.ToString());
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

    }
}
