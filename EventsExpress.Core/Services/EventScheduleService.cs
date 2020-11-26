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
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public EventScheduleService(AppDbContext context, IMapper mapper)
            : base(context)
        {
            _context = context;
            _mapper = mapper;
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
            var ev = Get(eventScheduleDTO.Id);
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

        public EventScheduleDTO EventScheduleById(Guid id) =>
            _mapper.Map<EventScheduleDTO>(
                 Get("Event.City.Country,Event.Photo,Event.Categories.Category")
                .FirstOrDefault(x => x.Id == id));

        public IEnumerable<EventScheduleDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<EventScheduleDTO>>(
                 Get("Event.City.Country,Event.Photo,Event.Owner,Event.Categories.Category")
                .Where(opt => opt.IsActive)
                .ToList());
        }

        public IEnumerable<EventScheduleDTO> GetUrgentEventSchedules()
        {
            return _mapper.Map<IEnumerable<EventScheduleDTO>>(
                 Get()
                .Where(x => x.LastRun == DateTime.Today && x.IsActive == true)
                .ToList());
        }

        public EventScheduleDTO EventScheduleByEventId(Guid eventId)
        {
            return _mapper.Map<EventScheduleDTO>(
                 Get()
                .FirstOrDefault(x => x.EventId == eventId));
        }
    }
}
