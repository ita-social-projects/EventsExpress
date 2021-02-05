using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.BaseService;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services
{
    public class EventService : BaseService<Event>, IEventService
    {
        private readonly IPhotoService _photoService;
        private readonly ILocationService _locationService;
        private readonly IMediator _mediator;
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEventScheduleService _eventScheduleService;

        public EventService(
            AppDbContext context,
            IMapper mapper,
            IMediator mediator,
            IPhotoService photoService,
            ILocationService locationService,
            IAuthService authService,
            IHttpContextAccessor httpContextAccessor,
            IEventScheduleService eventScheduleService)
            : base(context, mapper)
        {
            _photoService = photoService;
            _locationService = locationService;
            _mediator = mediator;
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
            _eventScheduleService = eventScheduleService;
        }

        public async Task AddUserToEvent(Guid userId, Guid eventId)
        {
            if (!Context.Events.Any(e => e.Id == eventId))
            {
                throw new EventsExpressException("Event not found!");
            }

            var ev = Context.Events
                .Include(e => e.Visitors)
                .First(e => e.Id == eventId);

            if (ev.MaxParticipants <= ev.Visitors.Count)
            {
                throw new EventsExpressException("To much participants!");
            }

            var us = Context.Users.Find(userId);
            if (us == null)
            {
                throw new EventsExpressException("User not found!");
            }

            Context.UserEvent.Add(new UserEvent
            {
                EventId = eventId,
                UserId = userId,
                UserStatusEvent = ev.IsPublic ? UserStatusEvent.Approved : UserStatusEvent.Pending,
            });

            await Context.SaveChangesAsync();
        }

        public async Task ChangeVisitorStatus(Guid userId, Guid eventId, UserStatusEvent status)
        {
            var userEvent = Context.UserEvent
                .Where(x => x.EventId == eventId && x.UserId == userId)
                .FirstOrDefault();

            userEvent.UserStatusEvent = status;
            await _mediator.Publish(new ParticipationMessage(userEvent.UserId, userEvent.EventId, status));

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

            if (uei != null)
            {
                Context.UserEventInventories.RemoveRange(uei);
            }

            var v = ev.Visitors?.FirstOrDefault(x => x.UserId == userId);

            if (v != null)
            {
                ev.Visitors.Remove(v);
                await Context.SaveChangesAsync();
            }
            else
            {
                throw new EventsExpressException("Visitor not found!");
            }
        }

        public async Task BlockEvent(Guid eventId)
        {
            var evnt = Context.Events.Find(eventId);
            if (evnt == null)
            {
                throw new EventsExpressException("Invalid event id");
            }

            evnt.IsBlocked = true;

            await Context.SaveChangesAsync();

            Context.EventOwners.Where(x => x.EventId == eventId).Select(x => x.UserId);
        }

        public async Task UnblockEvent(Guid eventId)
        {
            var evnt = Context.Events.Find(eventId);
            if (evnt == null)
            {
                throw new EventsExpressException("Invalid event Id");
            }

            evnt.IsBlocked = false;

            await Context.SaveChangesAsync();

            var userIds = Context.EventOwners.Where(x => x.EventId == eventId).Select(x => x.UserId);

            await _mediator.Publish(new UnblockedEventMessage(userIds, evnt.Id));
        }

        public async Task<Guid> Create(EventDto eventDTO)
        {
            eventDTO.DateFrom = (eventDTO.DateFrom == DateTime.MinValue) ? DateTime.Today : eventDTO.DateFrom;
            eventDTO.DateTo = (eventDTO.DateTo < eventDTO.DateFrom) ? eventDTO.DateFrom : eventDTO.DateTo;

            var locationDTO = Mapper.Map<EventDto, LocationDto>(eventDTO);
            var locationId = await _locationService.AddLocationToEvent(locationDTO);

            var ev = Mapper.Map<EventDto, Event>(eventDTO);
            ev.EventLocationId = locationId;

            if (ev.Owners == null)
            {
                ev.Owners = new List<EventOwner>();
            }

            ev.Owners.Add(new EventOwner() { UserId = CurrentUser().Id, EventId = eventDTO.Id });

            if (eventDTO.Photo == null)
            {
                ev.PhotoId = eventDTO.PhotoId;
            }
            else
            {
                try
                {
                    ev.Photo = await _photoService.AddPhoto(eventDTO.Photo);
                }
                catch (ArgumentException)
                {
                    throw new EventsExpressException("Invalid file");
                }
            }

            var eventCategories = eventDTO.Categories?
                .Select(x => new EventCategory { Event = ev, CategoryId = x.Id })
                .ToList();
            ev.Categories = eventCategories;

            var result = Insert(ev);

            eventDTO.Id = result.Id;

            await Context.SaveChangesAsync();

            if (eventDTO.IsReccurent)
            {
                await _eventScheduleService.Create(Mapper.Map<EventScheduleDto>(eventDTO));
            }

            await _mediator.Publish(new EventCreatedMessage(eventDTO));

            return result.Id;
        }

        public async Task<Guid> CreateNextEvent(Guid eventId)
        {
            var eventDTO = EventById(eventId);
            eventDTO.Inventories = null;
            var eventScheduleDTO = _eventScheduleService.EventScheduleByEventId(eventId);

            var ticksDiff = eventDTO.DateTo.Ticks - eventDTO.DateFrom.Ticks;
            eventDTO.Id = Guid.Empty;
            eventDTO.Owners = null;
            eventDTO.IsReccurent = false;
            eventDTO.DateFrom = eventScheduleDTO.NextRun;
            eventDTO.DateTo = eventDTO.DateFrom.AddTicks(ticksDiff);
            eventScheduleDTO.LastRun = eventDTO.DateTo;
            eventScheduleDTO.NextRun = DateTimeExtensions
                .AddDateUnit(eventScheduleDTO.Periodicity, eventScheduleDTO.Frequency, eventDTO.DateTo);

            var createResult = await Create(eventDTO);
            await _eventScheduleService.Edit(eventScheduleDTO);

            return createResult;
        }

        public async Task<Guid> Edit(EventDto e)
        {
            var ev = Context.Events
                .Include(e => e.Photo)
                .Include(e => e.EventLocation)
                .Include(e => e.Categories)
                    .ThenInclude(c => c.Category)
                .FirstOrDefault(x => x.Id == e.Id);

            if (e.Photo != null && ev.Photo != null)
            {
                await _photoService.Delete(ev.Photo.Id);
                try
                {
                    ev.Photo = await _photoService.AddPhoto(e.Photo);
                }
                catch (ArgumentException)
                {
                    throw new EventsExpressException("Invalid file");
                }
            }

            var locationDTO = Mapper.Map<EventDto, LocationDto>(e);
            var locationId = await _locationService.AddLocationToEvent(locationDTO);

            ev.Title = e.Title;
            ev.MaxParticipants = e.MaxParticipants;
            ev.Description = e.Description;
            ev.DateFrom = e.DateFrom;
            ev.DateTo = e.DateTo;
            ev.IsPublic = e.IsPublic;
            ev.EventLocationId = locationId;

            var eventCategories = e.Categories?.Select(x => new EventCategory { Event = ev, CategoryId = x.Id })
                .ToList();

            ev.Categories = eventCategories;

            await Context.SaveChangesAsync();

            return ev.Id;
        }

        public async Task<Guid> EditNextEvent(EventDto eventDTO)
        {
            var eventScheduleDTO = _eventScheduleService.EventScheduleByEventId(eventDTO.Id);
            eventScheduleDTO.LastRun = eventDTO.DateTo;
            eventScheduleDTO.NextRun = DateTimeExtensions
                .AddDateUnit(eventScheduleDTO.Periodicity, eventScheduleDTO.Frequency, eventDTO.DateTo);

            eventDTO.IsReccurent = false;
            eventDTO.Id = Guid.Empty;

            var createResult = await Create(eventDTO);
            await _eventScheduleService.Edit(eventScheduleDTO);

            return createResult;
        }

        public EventDto EventById(Guid eventId)
        {
            var res = Mapper.Map<EventDto>(
                Context.Events
                .Include(e => e.Photo)
                .Include(e => e.EventLocation)
                .Include(e => e.Owners)
                .ThenInclude(o => o.User)
                .ThenInclude(c => c.Photo)
                .Include(e => e.Categories)
                .ThenInclude(c => c.Category)
                .Include(e => e.Inventories)
                .ThenInclude(i => i.UnitOfMeasuring)
                .Include(e => e.Visitors)
                .ThenInclude(v => v.User)
                .ThenInclude(u => u.Photo)
                .FirstOrDefault(x => x.Id == eventId));

            return res;
        }

        public IEnumerable<EventDto> GetAll(EventFilterViewModel model, out int count)
        {
            var events = Context.Events
                .Include(e => e.Photo)
                .Include(e => e.EventLocation)
                .Include(e => e.Owners)
                    .ThenInclude(o => o.User)
                        .ThenInclude(c => c.Photo)
                .Include(e => e.Categories)
                    .ThenInclude(c => c.Category)
                .Include(e => e.Visitors)
                .AsNoTracking()
                .AsQueryable();

            events = !string.IsNullOrEmpty(model.KeyWord)
                ? events.Where(x => x.Title.Contains(model.KeyWord)
                    || x.Description.Contains(model.KeyWord))
                : events;

            events = (model.DateFrom != DateTime.MinValue)
                ? events.Where(x => x.DateFrom >= model.DateFrom)
                : events;

            events = (model.DateTo != DateTime.MinValue)
                ? events.Where(x => x.DateTo <= model.DateTo)
                : events;

            events = (model.OwnerId != null)
                ? events.Where(x => x.Owners.Any(c => c.UserId == model.OwnerId))
                : events;

            events = (model.VisitorId != null)
                ? events.Where(x => x.Visitors.Any(v => v.UserId == model.VisitorId))
                : events;

            events = (model.X != null && model.Y != null && model.Radius != null)
                ? events.Where(x => Math.Pow(x.EventLocation.Point.X - (double)model.X, 2) + Math.Pow(x.EventLocation.Point.Y - (double)model.Y, 2) - Math.Pow((double)model.Radius, 2) <= 0)
                : events;

            switch (model.Status)
            {
                case EventStatus.Active:
                    events = events.Where(x => !x.IsBlocked);
                    break;
                case EventStatus.Blocked:
                    events = events.Where(x => x.IsBlocked);
                    break;
            }

            if (model.Categories != null)
            {
                List<Guid> categoryIds = model.Categories
                    .Select(x => Guid.TryParse(x, out Guid item) ? item : Guid.Empty)
                    .Where(x => x != Guid.Empty)
                    .ToList();

                events = events.Where(x =>
                    x.Categories.Any(category =>
                        categoryIds.Contains(category.CategoryId)));
            }

            count = events.Count();

            var result = events.OrderBy(x => x.DateFrom)
                .Skip((model.Page - 1) * model.PageSize)
                .Take(model.PageSize)
                .ToList();

            return Mapper.Map<IEnumerable<EventDto>>(result);
        }

        public IEnumerable<EventDto> FutureEventsByUserId(Guid userId, PaginationViewModel paginationViewModel)
        {
            var filter = new EventFilterViewModel
            {
                OwnerId = userId,
                DateFrom = DateTime.Today,
                Page = paginationViewModel.Page,
                PageSize = paginationViewModel.PageSize,
            };

            var evnts = this.GetAll(filter, out int count);

            paginationViewModel.Count = count;

            return evnts;
        }

        public IEnumerable<EventDto> PastEventsByUserId(Guid userId, PaginationViewModel paginationViewModel)
        {
            var filter = new EventFilterViewModel
            {
                OwnerId = userId,
                DateTo = DateTime.Today,
                Page = paginationViewModel.Page,
                PageSize = paginationViewModel.PageSize,
            };

            var evnts = this.GetAll(filter, out int count);

            paginationViewModel.Count = count;

            return evnts;
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

            var evnts = this.GetAll(filter, out int count);

            paginationViewModel.Count = count;

            return evnts;
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

            var evnts = this.GetAll(filter, out int count);

            paginationViewModel.Count = count;

            return evnts;
        }

        public IEnumerable<EventDto> GetEvents(List<Guid> eventIds, PaginationViewModel paginationViewModel)
        {
            var events = Context.Events
                .Include(e => e.Photo)
                .Include(e => e.EventLocation)
                .Include(e => e.Owners)
                    .ThenInclude(o => o.User)
                        .ThenInclude(c => c.Photo)
                .Include(e => e.Categories)
                    .ThenInclude(c => c.Category)
                .Include(e => e.Visitors)
                .Where(x => eventIds.Contains(x.Id))
                .AsNoTracking()
                .AsQueryable();

            paginationViewModel.Count = events.Count();

            events = events.Skip((paginationViewModel.Page - 1) * paginationViewModel.PageSize)
                .Take(paginationViewModel.PageSize);

            return Mapper.Map<IEnumerable<EventDto>>(events);
        }

        public async Task SetRate(Guid userId, Guid eventId, byte rate)
        {
            var ev = Context.Events
                .Include(e => e.Rates)
                .FirstOrDefault(e => e.Id == eventId);

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

        private UserDto CurrentUser() =>
           _authService.GetCurrentUser(_httpContextAccessor.HttpContext.User);
    }
}
