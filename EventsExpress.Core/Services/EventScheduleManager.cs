using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services
{
    public class EventScheduleManager : BaseService<EventSchedule>, IEventScheduleManager
    {
        private readonly ISecurityContext _securityContextService;

        public EventScheduleManager(AppDbContext context, IMapper mapper, ISecurityContext securityContextService)
            : base(context, mapper)
        {
            _securityContextService = securityContextService;
        }

        public IEnumerable<EventScheduleDto> GetAll()
        {
            return Mapper.Map<IEnumerable<EventScheduleDto>>(
                Context.EventSchedules
                    .Include(es => es.Event)
                        .ThenInclude(e => e.Organizers)
                    .Include(es => es.Event)
                        .ThenInclude(e => e.StatusHistory)
                    .Include(es => es.Event)
                    .Where(opt => opt.IsActive &&
                        opt.Event.Organizers.Any(o => o.UserId == CurrentUserId()) &&
                        opt.Event.StatusHistory.OrderBy(h => h.CreatedOn).Last().EventStatus != EventStatus.Draft)
                    .ToList());
        }

        public EventScheduleDto EventScheduleById(Guid eventScheduleId)
        {
            var res = Context.EventSchedules
                .Include(es => es.Event)
                .Include(es => es.Event)
                    .ThenInclude(e => e.Organizers)
                        .ThenInclude(d => d.User)
                .FirstOrDefault(x => x.Id == eventScheduleId);

            return Mapper.Map<EventSchedule, EventScheduleDto>(res);
        }

        public EventScheduleDto EventScheduleByEventId(Guid eventId) =>
            Mapper.Map<EventSchedule, EventScheduleDto>(
                 Context.EventSchedules
                .FirstOrDefault(x => x.EventId == eventId));

        public IEnumerable<EventScheduleDto> GetUrgentEventSchedules()
        {
            return Mapper.Map<IEnumerable<EventScheduleDto>>(
                 Context.EventSchedules
                .Where(x => x.LastRun == DateTime.Today && x.IsActive)
                .ToList());
        }

        public async Task<Guid> Create(EventScheduleDto eventScheduleDTO)
        {
            var eventScheduleEntity = Mapper.Map<EventScheduleDto, EventSchedule>(eventScheduleDTO);

            var result = Insert(eventScheduleEntity);
            await Context.SaveChangesAsync();

            return result.Id;
        }

        public async Task<Guid> Edit(EventScheduleDto eventScheduleDTO)
        {
            var ev = Context.EventSchedules.Find(eventScheduleDTO.Id);

            if (ev == null)
            {
                throw new EventsExpressException("Not found");
            }
            else
            {
                ev.Frequency = eventScheduleDTO.Frequency;
                ev.Periodicity = eventScheduleDTO.Periodicity;
                ev.LastRun = eventScheduleDTO.LastRun;
                ev.NextRun = eventScheduleDTO.NextRun;
                ev.IsActive = eventScheduleDTO.IsActive;
                ev.EventId = eventScheduleDTO.EventId;

                await Context.SaveChangesAsync();
            }

            return eventScheduleDTO.Id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            var eventSchedule = Context.EventSchedules.Find(id);
            if (eventSchedule == null)
            {
                throw new EventsExpressException("Not found");
            }

            var result = Delete(eventSchedule);

            await Context.SaveChangesAsync();

            return result.Id;
        }

        public async Task<Guid> CancelEvents(Guid eventId)
        {
            var eventScheduleDTO = EventScheduleByEventId(eventId);
            eventScheduleDTO.IsActive = false;

            return await Edit(eventScheduleDTO);
        }

        public async Task<Guid> CancelNextEvent(Guid eventId)
        {
            var eventScheduleDTO = EventScheduleByEventId(eventId);
            eventScheduleDTO.LastRun = eventScheduleDTO.NextRun;
            eventScheduleDTO.NextRun = DateTimeExtensions
                .AddDateUnit(eventScheduleDTO.Periodicity, eventScheduleDTO.Frequency, eventScheduleDTO.LastRun);

            return await Edit(eventScheduleDTO);
        }

        private Guid CurrentUserId() =>
           _securityContextService.GetCurrentUserId();
    }
}
