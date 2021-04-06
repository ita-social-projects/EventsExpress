using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.IServices;
using EventsExpress.Db.BaseService;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services
{
    public class EventScheduleService : BaseService<EventSchedule>, IEventScheduleService
    {
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventScheduleService(AppDbContext context, IMapper mapper, IAuthService authService, IHttpContextAccessor httpContextAccessor)
            : base(context, mapper)
        {
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<EventScheduleDto> GetAll()
        {
            return Mapper.Map<IEnumerable<EventScheduleDto>>(
                Context.EventSchedules
                    .Include(es => es.Event)
                        .ThenInclude(e => e.Owners)
                    .Include(es => es.Event)
                    .Where(opt => opt.IsActive &&
                        opt.Event.Owners.Any(o => o.UserId == CurrentUser().Id))
                    .ToList());
        }

        public EventScheduleDto EventScheduleById(Guid eventScheduleId)
        {
            var res = Context.EventSchedules
                .Include(es => es.Event)
                .Include(es => es.Event)
                    .ThenInclude(e => e.Owners)
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
            ev.Frequency = eventScheduleDTO.Frequency;
            ev.Periodicity = eventScheduleDTO.Periodicity;
            ev.LastRun = eventScheduleDTO.LastRun;
            ev.NextRun = eventScheduleDTO.NextRun;
            ev.IsActive = eventScheduleDTO.IsActive;
            ev.EventId = eventScheduleDTO.EventId;

            await Context.SaveChangesAsync();

            return eventScheduleDTO.Id;
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

        private UserDto CurrentUser() =>
           _authService.GetCurrentUser(_httpContextAccessor.HttpContext.User);
    }
}
