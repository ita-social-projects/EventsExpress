using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Enums;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.Db.Migrations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace EventsExpress.Core.Services
{
    public class EventService : BaseService<Event>, IEventService
    {
        private readonly IPhotoService _photoService;
        private readonly ILocationManager _locationManager;
        private readonly IMediator _mediator;
        private readonly IEventScheduleService _eventScheduleService;
        private readonly ISecurityContext _securityContextService;

        public EventService(
            AppDbContext context,
            IMapper mapper,
            IMediator mediator,
            IPhotoService photoService,
            ILocationManager locationManager,
            IEventScheduleService eventScheduleService,
            ISecurityContext securityContextService)
            : base(context, mapper)
        {
            _photoService = photoService;
            _locationManager = locationManager;
            _mediator = mediator;
            _eventScheduleService = eventScheduleService;
            _securityContextService = securityContextService;
        }

        private static Point MapPointFromFilter(EventFilterViewModel model)
        {
            return new Point(model.X ?? 0, model.Y ?? 0) { SRID = 4326 };
        }

        private static List<Event> GetPageOfSortedEventList(IQueryable<Event> events, int page, int pageSize)
        {
            return events.OrderBy(e => e.DateFrom).Page(page, pageSize).ToList();
        }

        public async Task AddUserToEvent(Guid userId, Guid eventId)
        {
            if (!Context.Events.Any(e => e.Id == eventId))
            {
                throw new EventsExpressException("Event not found!");
            }

            var ev = Context.Events
                .Include(e => e.EventAudience)
                .Include(e => e.Visitors)
                .First(e => e.Id == eventId);

            if (ev.MaxParticipants <= ev.Visitors.Count)
            {
                throw new EventsExpressException("Too much participants!");
            }

            var us = await Context.Users.FindAsync(userId);
            if (us == null)
            {
                throw new EventsExpressException("User not found!");
            }

            if (ev.EventAudience?.IsOnlyForAdults == true)
            {
                int userAge = DateTime.Today.GetDifferenceInYears(us.Birthday);
                if (userAge < 18)
                {
                    throw new EventsExpressException("User does not meet age requirements!");
                }
            }

            Context.UserEvent.Add(new UserEvent
            {
                EventId = eventId,
                UserId = userId,
                UserStatusEvent = ev.IsPublic switch
                {
                    null => UserStatusEvent.Denied,
                    true => UserStatusEvent.Approved,
                    false => UserStatusEvent.Pending,
                },
            });

            await Context.SaveChangesAsync();
        }

        public async Task ChangeVisitorStatus(Guid userId, Guid eventId, UserStatusEvent status)
        {
            var userEvent = Context.UserEvent
                .First(x => x.EventId == eventId && x.UserId == userId);

            userEvent.UserStatusEvent = status;

            Context.UserEvent.Update(userEvent);
            await Context.SaveChangesAsync();
        }

        public async Task DeleteUserFromEvent(Guid userId, Guid eventId)
        {
            var ev = Context.Events
                .Include(e => e.Visitors)
                .FirstOrDefault(e => e.Id == eventId);

            if (ev == null)
            {
                throw new EventsExpressException("Event not found!");
            }

            var uei = Context.UserEventInventories.Where(ue => ue.UserId == userId).ToArray();

            Context.UserEventInventories.RemoveRange(uei);

            var v = ev.Visitors?.FirstOrDefault(x => x.UserId == userId);

            if (v != null)
            {
                ev.Visitors?.Remove(v);
                await Context.SaveChangesAsync();
            }
            else
            {
                throw new EventsExpressException("Visitor not found!");
            }
        }

        public Guid CreateDraft()
        {
            var ev = new Event();
            ev.StatusHistory = new List<EventStatusHistory>
            {
                new EventStatusHistory
                {
                    EventStatus = EventStatus.Draft,
                    CreatedOn = DateTime.UtcNow,
                    UserId = CurrentUserId(),
                },
            };
            ev.Organizers = new List<EventOrganizer>
            {
                new EventOrganizer
                {
                    UserId = CurrentUserId(),
                    EventId = ev.Id,
                },
            };

            var result = Insert(ev);
            Context.SaveChanges();

            return result.Id;
        }

        public async Task<Guid> Create(EventDto eventDto)
        {
            eventDto.DateFrom = (eventDto.DateFrom == DateTime.MinValue) ? DateTime.Today : eventDto.DateFrom;
            eventDto.DateTo = (eventDto.DateTo < eventDto.DateFrom) ? eventDto.DateFrom : eventDto.DateTo;

            var locationDto = eventDto.Location;
            var locationId = _locationManager.Create(locationDto);

            var ev = Mapper.Map<EventDto, Event>(eventDto);
            ev.LocationId = locationId;

            ev.StatusHistory = new List<EventStatusHistory>
            {
                new EventStatusHistory
                {
                    EventStatus = EventStatus.Active,
                    CreatedOn = DateTime.UtcNow,
                    UserId = CurrentUserId(),
                },
            };

            ev.Organizers = new List<EventOrganizer>
            {
                new EventOrganizer
                {
                    UserId = CurrentUserId(),
                    EventId = eventDto.Id,
                },
            };

            var eventCategories = eventDto.Categories?
                .Select(x => new EventCategory { Event = ev, CategoryId = x.Id })
                .ToList();
            ev.Categories = eventCategories;

            var result = Insert(ev);

            eventDto.Id = result.Id;

            await Context.SaveChangesAsync();
            await _photoService.ChangeTempToImagePhoto(eventDto.Id);

            return result.Id;
        }

        public async Task<Guid> CreateNextEvent(Guid eventId)
        {
            var eventDto = EventById(eventId);

            var eventScheduleDto = _eventScheduleService.EventScheduleByEventId(eventId);

            long ticksDiff = 0;
            if (eventDto.DateTo != null && eventDto.DateFrom != null)
            {
                ticksDiff = eventDto.DateTo.Value.Ticks - eventDto.DateFrom.Value.Ticks;
            }

            eventDto.Id = Guid.Empty;
            eventDto.Organizers = null;
            eventDto.Inventories = null;
            eventDto.IsReccurent = false;
            eventDto.DateFrom = eventScheduleDto.NextRun;
            eventDto.DateTo = eventDto.DateFrom.Value.AddTicks(ticksDiff);
            eventScheduleDto.LastRun = eventDto.DateTo.Value;
            eventScheduleDto.NextRun = DateTimeExtensions
                .AddDateUnit(eventScheduleDto.Periodicity, eventScheduleDto.Frequency, eventDto.DateTo.Value);

            await _eventScheduleService.Edit(eventScheduleDto);

            var createResult = await Create(eventDto);

            return createResult;
        }

        public async Task<Guid> Edit(EventDto eventDto)
        {
            var ev = Context.Events
                .Include(e => e.Categories)
                    .ThenInclude(c => c.Category)
                .Include(e => e.EventSchedule)
                .Include(e => e.EventAudience)
                .First(x => x.Id == eventDto.Id);

            if (ev.LocationId == null)
            {
                var locationId = _locationManager.Create(eventDto.Location);
                ev.LocationId = locationId;
            }
            else
            {
                eventDto.Location.Id = ev.LocationId.Value;
                _locationManager.EditLocation(eventDto.Location);
            }

            if (eventDto.IsReccurent)
            {
                if (eventDto.EventStatus == EventStatus.Draft)
                {
                    if (ev.EventSchedule == null)
                    {
                        await _eventScheduleService.Create(Mapper.Map<EventScheduleDto>(eventDto));
                    }
                    else
                    {
                        var eventScheduleDto = Mapper.Map<EventScheduleDto>(eventDto);
                        eventScheduleDto.Id = ev.EventSchedule.Id;
                        await _eventScheduleService.Edit(eventScheduleDto);
                    }
                }
            }
            else
            {
                if (ev.EventSchedule != null)
                {
                    await _eventScheduleService.Delete(ev.EventSchedule.Id);
                }
            }

            ev.Title = eventDto.Title;
            ev.MaxParticipants = eventDto.MaxParticipants;
            ev.Description = eventDto.Description;
            ev.DateFrom = eventDto.DateFrom;
            ev.DateTo = eventDto.DateTo;
            ev.IsPublic = eventDto.IsPublic;

            if (eventDto.EventStatus == EventStatus.Draft)
            {
                ev.EventAudience ??= new EventAudience();
                ev.EventAudience.IsOnlyForAdults = eventDto.IsOnlyForAdults;
            }

            var eventCategories = eventDto.Categories?.Select(x => new EventCategory { Event = ev, CategoryId = x.Id })
                .ToList();

            ev.Categories = eventCategories;

            await Context.SaveChangesAsync();
            await _photoService.ChangeTempToImagePhoto(eventDto.Id);
            await _mediator.Publish(new OwnEventMessage(eventDto.Id));
            await _mediator.Publish(new JoinedEventMessage(eventDto.Id));

            return ev.Id;
        }

        public async Task<Guid> Publish(Guid eventId)
        {
            var ev = Context.Events
               .Include(e => e.Location)
               .Include(e => e.StatusHistory)
               .Include(e => e.EventAudience)
               .Include(e => e.Categories)
                   .ThenInclude(c => c.Category)
               .First(x => x.Id == eventId);

            ev.StatusHistory.Add(
                    new EventStatusHistory
                    {
                        EventStatus = EventStatus.Active,
                        CreatedOn = DateTime.UtcNow,
                        UserId = CurrentUserId(),
                    });
            await Context.SaveChangesAsync();
            EventDto dtos = Mapper.Map<Event, EventDto>(ev);
            return ev.Id;
        }

        public async Task<Guid> EditNextEvent(EventDto eventDto)
        {
            var eventScheduleDto = _eventScheduleService.EventScheduleByEventId(eventDto.Id);
            if (eventDto.DateTo != null)
            {
                eventScheduleDto.LastRun = eventDto.DateTo.Value;
                eventScheduleDto.NextRun = DateTimeExtensions
                    .AddDateUnit(eventScheduleDto.Periodicity, eventScheduleDto.Frequency, eventDto.DateTo.Value);
            }

            await _eventScheduleService.Edit(eventScheduleDto);

            eventDto.IsReccurent = false;
            eventDto.Id = Guid.Empty;

            var createResult = await Create(eventDto);

            return createResult;
        }

        public EventDto EventById(Guid eventId)
        {
            var events = Context.Events
                .Include(e => e.EventSchedule)
                .Include(e => e.Location)
                .Include(e => e.EventAudience)
                .Include(e => e.Organizers)
                    .ThenInclude(o => o.User)
                        .ThenInclude(u => u.Relationships)
                .Include(e => e.Categories)
                    .ThenInclude(c => c.Category)
                .Include(e => e.Inventories)
                    .ThenInclude(i => i.UnitOfMeasuring)
                .Include(e => e.Visitors)
                    .ThenInclude(v => v.User)
                        .ThenInclude(u => u.Relationships)
                .Include(e => e.StatusHistory)
                .AsNoTracking()
                .AsQueryable();
            var ev = events.FirstOrDefault(x => x.Id == eventId);
            return Mapper.Map<EventDto>(ev);
        }

        public IEnumerable<EventDto> GetAll(EventFilterViewModel model, out int count)
        {
            var events = Context.Events
                .Include(e => e.Location)
                .Include(e => e.EventAudience)
                .Include(e => e.Organizers)
                    .ThenInclude(o => o.User)
                .Include(e => e.Categories)
                    .ThenInclude(c => c.Category)
                .Include(e => e.Visitors)
                    .ThenInclude(v => v.User)
                        .ThenInclude(u => u.Relationships)
                .Include(e => e.StatusHistory)
                .AsNoTracking();

            events = ApplyEventFilters(events, model);
            count = events.Count();

            if (model.Order == EventOrderCriteria.StartSoon)
            {
                events = events.OrderBy(e => e.DateFrom);
            }
            else if (model.Order == EventOrderCriteria.RecentlyPublished)
            {
                events = events.OrderByDescending(e => e.StatusHistory
                    .OrderBy(sh => sh.CreatedOn)
                    .First(sh => sh.EventStatus != EventStatus.Draft)
                    .CreatedOn);
            }

            var eventPage = events.Page(model.Page, model.PageSize).ToList();
            return Mapper.Map<IEnumerable<EventDto>>(eventPage);
        }

        public IEnumerable<EventDto> GetAllDraftEvents(int page, int pageSize, out int count)
        {
            var events = Context.Events
                .Include(e => e.Location)
                .Include(e => e.Organizers)
                    .ThenInclude(o => o.User)
                .Include(e => e.Categories)
                    .ThenInclude(c => c.Category)
                .Include(e => e.Visitors)
                .Include(e => e.StatusHistory)
                .AsNoTracking();

            events = events.Where(e => e.StatusHistory.OrderBy(h => h.CreatedOn)
                .Last().EventStatus == EventStatus.Draft);
            events = events.Where(e => e.Organizers.Any(o => o.UserId == CurrentUserId()));

            count = events.Count();

            var eventPage = GetPageOfSortedEventList(events, page, pageSize);
            return Mapper.Map<IEnumerable<EventDto>>(eventPage);
        }

        public IEnumerable<EventDto> FutureEventsByUserId(Guid userId, PaginationViewModel paginationViewModel)
        {
            var filter = new EventFilterViewModel
            {
                OrganizerId = userId,
                DateFrom = DateTime.Today,
                Page = paginationViewModel.Page,
                PageSize = paginationViewModel.PageSize,
            };

            var events = this.GetAll(filter, out int count);

            paginationViewModel.Count = count;

            return events;
        }

        public IEnumerable<EventDto> PastEventsByUserId(Guid userId, PaginationViewModel paginationViewModel)
        {
            var filter = new EventFilterViewModel
            {
                OrganizerId = userId,
                DateTo = DateTime.Today,
                Page = paginationViewModel.Page,
                PageSize = paginationViewModel.PageSize,
            };

            var events = this.GetAll(filter, out int count);

            paginationViewModel.Count = count;

            return events;
        }

        public IEnumerable<EventDto> VisitedEventsByUserId(Guid userId, PaginationViewModel paginationViewModel)
        {
            var filter = new EventFilterViewModel
            {
                VisitorId = userId,
                DateTo = DateTime.Today,
                Page = paginationViewModel.Page,
                PageSize = paginationViewModel.PageSize,
            };

            var events = this.GetAll(filter, out int count);

            paginationViewModel.Count = count;

            return events;
        }

        public IEnumerable<EventDto> EventsToGoByUserId(Guid userId, PaginationViewModel paginationViewModel)
        {
            var filter = new EventFilterViewModel
            {
                VisitorId = userId,
                DateFrom = DateTime.Today,
                Page = paginationViewModel.Page,
                PageSize = paginationViewModel.PageSize,
            };

            var events = this.GetAll(filter, out int count);

            paginationViewModel.Count = count;

            return events;
        }

        public IEnumerable<EventDto> GetEvents(List<Guid> eventIds, PaginationViewModel paginationViewModel)
        {
            var events = Context.Events
                .Include(e => e.Location)
                .Include(e => e.Organizers)
                    .ThenInclude(o => o.User)
                .Include(e => e.Categories)
                    .ThenInclude(c => c.Category)
                .Include(e => e.Visitors)
                .Include(e => e.StatusHistory)
                .Include(e => e.EventAudience)
                .Where(x => eventIds.Contains(x.Id))
                .AsNoTracking();

            paginationViewModel.Count = events.Count();

            var eventPage = events.Page(paginationViewModel.Page, paginationViewModel.PageSize);
            return Mapper.Map<IEnumerable<EventDto>>(eventPage);
        }

        public async Task SetRate(Guid userId, Guid eventId, byte rate)
        {
            var ev = Context.Events
                .Include(e => e.Rates)
                .Single(e => e.Id == eventId);

            ev.Rates ??= new List<Rate>();

            var currentRate = ev.Rates.FirstOrDefault(x => x.UserFromId == userId && x.EventId == eventId);

            if (currentRate == null)
            {
                ev.Rates.Add(new Rate { EventId = eventId, UserFromId = userId, Score = rate });
            }
            else
            {
                currentRate.Score = rate;
            }

            await Context.SaveChangesAsync();
        }

        public byte GetRateFromUser(Guid userId, Guid eventId)
        {
            return Context.Rates
                .FirstOrDefault(r => r.UserFromId == userId && r.EventId == eventId)
                ?.Score ?? 0;
        }

        public double GetRate(Guid eventId)
        {
            try
            {
                return Context.Rates
                    .Where(r => r.EventId == eventId)
                    .Average(r => r.Score);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool UserIsVisitor(Guid userId, Guid eventId) =>
                 Context.Events
                    .Include(e => e.Visitors)
                    .FirstOrDefault(e => e.Id == eventId)?.Visitors
                    ?.FirstOrDefault(v => v.UserId == userId) != null;

        public bool Exists(Guid eventId) => Context.Events.Find(eventId) != null;

        private Guid CurrentUserId() =>
           _securityContextService.GetCurrentUserId();

        private IQueryable<Event> ApplyEventFilters(IQueryable<Event> events, EventFilterViewModel model)
        {
            var eventsFilters = events
                .Filters()
                    .AddFilter(e => e.StatusHistory.OrderBy(h => h.CreatedOn)
                        .Last().EventStatus != EventStatus.Draft)
                .Then()
                    .If(!string.IsNullOrEmpty(model.KeyWord))
                    .AddFilter(e => e.Title.Contains(model.KeyWord)
                        || e.Description.Contains(model.KeyWord))
                .Then()
                    .If(model.DateFrom != DateTime.MinValue)
                    .AddFilter(e => e.DateFrom >= model.DateFrom)
                .Then()
                    .If(model.DateTo != DateTime.MinValue)
                    .AddFilter(e => e.DateTo <= model.DateTo)
                .Then()
                    .IfNotNull(model.OrganizerId)
                    .AddFilter(e => e.Organizers.Any(o => o.UserId == model.OrganizerId))
                .Then()
                    .IfNotNull(model.VisitorId)
                    .AddFilter(e => e.Visitors.Any(v => v.UserId == model.VisitorId))
                .Then()
                    .IfNotNull(model.X, model.Y, model.Radius)
                    .AddFilter(e => e.Location.Point
                        .Distance(MapPointFromFilter(model)) <= model.Radius * 1000)
                .Then()
                    .IfNotNull(model.LocationType)
                    .AddFilter(e => e.Location.Type == model.LocationType)
                .Then()
                    .IfNotNull(model.IsOnlyForAdults)
                    .AddFilter(e => e.EventAudience.IsOnlyForAdults == model.IsOnlyForAdults)
                .Then()
                    .IfNotNull(model.Statuses)
                    .AddFilter(e => model.Statuses.Contains(
                        e.StatusHistory.OrderBy(h => h.CreatedOn).Last().EventStatus))
                .Then()
                    .IfNotNull(model.Organizers)
                    .AddFilter(e => e.Organizers.Any(o => model.Organizers.Contains(o.UserId)))
                .Then()
                    .IfNotNull(model.Categories)
                    .AddFilter(e => e.Categories.Any(
                        c => model.Categories.Contains(c.CategoryId.ToString())))
                .Then()
                    .IfNotNull(model.DisplayUserEvents)
                    .AddFilter(e => e.Visitors.Any(v => v.UserId == CurrentUserId()))
                .Then()
                    .If(model.DisplayUserEvents == UserToEventRelation.GoingToVisit)
                    .AddFilter(e => e.DateFrom >= DateTime.Today)
                .Then()
                    .If(model.DisplayUserEvents == UserToEventRelation.Visited)
                    .AddFilter(e => e.DateTo <= DateTime.Today)
                .Then()
                    .If(model.Bookmarked)
                    .AddFilter(e => e.EventBookmarks.Any(b => b.UserFromId == CurrentUserId()));

            return eventsFilters.Apply();
        }
    }
}
