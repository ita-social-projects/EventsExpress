using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Extensions;
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

        public async Task<Guid> CancelEvents(Guid eventId)
        {
            if (eventId != Guid.Empty)
            {
                var eventScheduleDTO = EventScheduleByEventId(eventId);
                eventScheduleDTO.IsActive = false;

                return await Edit(eventScheduleDTO);
            }

            throw new EventsExpressException("Id is null");
        }

        public async Task<Guid> CancelNextEvent(Guid eventId)
        {
            if (eventId != Guid.Empty)
            {
                var eventScheduleDTO = EventScheduleByEventId(eventId);
                eventScheduleDTO.LastRun = eventScheduleDTO.NextRun;
                eventScheduleDTO.NextRun = DateTimeExtensions
                    .AddDateUnit(eventScheduleDTO.Periodicity, eventScheduleDTO.Frequency, eventScheduleDTO.LastRun);

                return await Edit(eventScheduleDTO);
            }

            throw new EventsExpressException("Id is null");
        }

        public async Task<Guid> Create(EventScheduleDTO eventScheduleDTO)
        {
            var eventScheduleEntity = _mapper.Map<EventScheduleDTO, EventSchedule>(eventScheduleDTO);
            eventScheduleEntity.CreatedBy = eventScheduleDTO.CreatedBy;

            var result = Insert(eventScheduleEntity);
            await _context.SaveChangesAsync();

            return result.Id;
        }

        public async Task<Guid> Edit(EventScheduleDTO eventScheduleDTO)
        {
            if (eventScheduleDTO.Id != Guid.Empty)
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

                return eventScheduleDTO.Id;
            }

            throw new EventsExpressException("Id not found");
        }

        public EventScheduleDTO EventScheduleById(Guid id)
        {
            var res = _context.EventSchedules
                .Include(es => es.Event)
                    .ThenInclude(e => e.City)
                        .ThenInclude(c => c.Country)
                .Include(es => es.Event)
                    .ThenInclude(e => e.Photo)
                .FirstOrDefault(x => x.Id == id);

            return _mapper.Map<EventSchedule, EventScheduleDTO>(res);
        }

        public IEnumerable<EventScheduleDTO> GetAll()
        {
            return _mapper.Map<IEnumerable<EventScheduleDTO>>(
                _context.EventSchedules
                    .Include(es => es.Event)
                        .ThenInclude(e => e.City)
                            .ThenInclude(c => c.Country)
                    .Include(es => es.Event)
                        .ThenInclude(e => e.Photo)
                    .Where(opt => opt.IsActive)
                    .ToList());
        }

        public IEnumerable<EventScheduleDTO> GetUrgentEventSchedules()
        {
            return _mapper.Map<IEnumerable<EventScheduleDTO>>(
                 _context.EventSchedules
                .Where(x => x.LastRun == DateTime.Today && x.IsActive == true)
                .ToList());
        }

        public EventScheduleDTO EventScheduleByEventId(Guid eventId) =>
            _mapper.Map<EventSchedule, EventScheduleDTO>(
                 _context.EventSchedules
                .FirstOrDefault(x => x.EventId == eventId));
    }
}
