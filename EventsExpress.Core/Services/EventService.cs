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
        private readonly IMediator _mediator;
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEventScheduleService _eventScheduleService;

        public EventService(
            AppDbContext context,
            IMapper mapper,
            IMediator mediator,
            IPhotoService photoSrv,
            IAuthService authService,
            IHttpContextAccessor httpContextAccessor,
            IEventScheduleService eventScheduleService)
            : base(context, mapper)
        {
            _photoService = photoSrv;
            _mediator = mediator;
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
            _eventScheduleService = eventScheduleService;
        }

        private UserDTO CurrentUser { get => _authService.GetCurrentUser(_httpContextAccessor.HttpContext.User); }

        public async Task AddUserToEvent(Guid userId, Guid eventId)
        {
            if (!_context.Events.Any(e => e.Id == eventId))
            {
                throw new EventsExpressException("Event not found!");
            }

            var ev = _context.Events
                .Include(e => e.Visitors)
                .First(e => e.Id == eventId);

            if (ev.MaxParticipants <= ev.Visitors.Count)
            {
                throw new EventsExpressException("To much participants!");
            }

            var us = _context.Users.Find(userId);
            if (us == null)
            {
                throw new EventsExpressException("User not found!");
            }

            _context.UserEvent.Add(new UserEvent
            {
                EventId = eventId,
                UserId = userId,
                UserStatusEvent = ev.IsPublic ? UserStatusEvent.Approved : UserStatusEvent.Pending,
            });

            await _context.SaveChangesAsync();
        }

        public async Task ChangeVisitorStatus(Guid userId, Guid eventId, UserStatusEvent status)
        {
            var userEvent = _context.UserEvent
                .Where(x => x.EventId == eventId && x.UserId == userId)
                .FirstOrDefault();

            userEvent.UserStatusEvent = status;
            await _mediator.Publish(new ParticipationMessage(userEvent.UserId, userEvent.EventId, status));

            _context.UserEvent.Update(userEvent);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserFromEvent(Guid userId, Guid eventId)
        {
            var ev = _context.Events
                .Include(e => e.Visitors)
                .FirstOrDefault(e => e.Id == eventId);

            if (ev == null)
            {
                throw new EventsExpressException("Event not found!");
            }

            var v = ev.Visitors?.FirstOrDefault(x => x.UserId == userId);

            if (v != null)
            {
                ev.Visitors.Remove(v);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new EventsExpressException("Visitor not found!");
            }
        }

        public async Task BlockEvent(Guid id)
        {
            var evnt = _context.Events.Find(id);
            if (evnt == null)
            {
                throw new EventsExpressException("Invalid event id");
            }

            evnt.IsBlocked = true;

            await _context.SaveChangesAsync();

            var userIds = _context.EventOwners.Where(x => x.EventId == id).Select(x => x.UserId);
        }

        public async Task UnblockEvent(Guid eId)
        {
            var evnt = _context.Events.Find(eId);
            if (evnt == null)
            {
                throw new EventsExpressException("Invalid event Id");
            }

            evnt.IsBlocked = false;

            await _context.SaveChangesAsync();

            var userIds = _context.EventOwners.Where(x => x.EventId == eId).Select(x => x.UserId);

            await _mediator.Publish(new UnblockedEventMessage(userIds, evnt.Id));
        }

        public async Task<Guid> Create(EventDTO eventDTO)
        {
            eventDTO.DateFrom = (eventDTO.DateFrom == DateTime.MinValue) ? DateTime.Today : eventDTO.DateFrom;
            eventDTO.DateTo = (eventDTO.DateTo < eventDTO.DateFrom) ? eventDTO.DateFrom : eventDTO.DateTo;

            var ev = _mapper.Map<EventDTO, Event>(eventDTO);
            ev.Owners.Add(new EventOwner() { UserId = CurrentUser.Id, EventId = eventDTO.Id });

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
            ev.CreatedBy = CurrentUser.Id;
            ev.ModifiedBy = CurrentUser.Id;
            ev.ModifiedDateTime = DateTime.UtcNow;

            var result = Insert(ev);

            eventDTO.Id = result.Id;

            await _context.SaveChangesAsync();

            if (eventDTO.IsReccurent)
            {
                await _eventScheduleService.Create(_mapper.Map<EventScheduleDTO>(eventDTO));
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
            eventScheduleDTO.ModifiedBy = CurrentUser.Id;
            eventScheduleDTO.ModifiedDateTime = DateTime.UtcNow;
            eventScheduleDTO.LastRun = eventDTO.DateTo;
            eventScheduleDTO.NextRun = DateTimeExtensions
                .AddDateUnit(eventScheduleDTO.Periodicity, eventScheduleDTO.Frequency, eventDTO.DateTo);

            var createResult = await Create(eventDTO);
            await _eventScheduleService.Edit(eventScheduleDTO);

            return createResult;
        }

        public async Task<Guid> Edit(EventDTO e)
        {
            var ev = _context.Events
                .Include(e => e.Photo)
                .Include(e => e.Categories)
                    .ThenInclude(c => c.Category)
                .FirstOrDefault(x => x.Id == e.Id);

            ev.Title = e.Title;
            ev.MaxParticipants = e.MaxParticipants;
            ev.Description = e.Description;
            ev.DateFrom = e.DateFrom;
            ev.DateTo = e.DateTo;
            ev.CityId = e.CityId;
            ev.ModifiedBy = CurrentUser.Id;
            ev.ModifiedDateTime = DateTime.UtcNow;
            ev.IsPublic = e.IsPublic;

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

            var eventCategories = e.Categories?.Select(x => new EventCategory { Event = ev, CategoryId = x.Id })
                .ToList();

            ev.Categories = eventCategories;

            await _context.SaveChangesAsync();

            return ev.Id;
        }

        public async Task<Guid> EditNextEvent(EventDTO eventDTO)
        {
            var eventScheduleDTO = _eventScheduleService.EventScheduleByEventId(eventDTO.Id);
            eventScheduleDTO.ModifiedBy = CurrentUser.Id;
            eventScheduleDTO.ModifiedDateTime = DateTime.UtcNow;
            eventScheduleDTO.LastRun = eventDTO.DateTo;
            eventScheduleDTO.NextRun = DateTimeExtensions
                .AddDateUnit(eventScheduleDTO.Periodicity, eventScheduleDTO.Frequency, eventDTO.DateTo);

            eventDTO.IsReccurent = false;
            eventDTO.Id = Guid.Empty;

            var createResult = await Create(eventDTO);
            await _eventScheduleService.Edit(eventScheduleDTO);

            return createResult;
        }

        public EventDTO EventById(Guid eventId) =>
            _mapper.Map<EventDTO>(
                _context.Events
                .Include(e => e.Photo)
                .Include(e => e.Owners)
                    .ThenInclude(o => o.User)
                        .ThenInclude(c => c.Photo)
                .Include(e => e.City)
                    .ThenInclude(o => o.Country)
                .Include(e => e.Categories)
                    .ThenInclude(c => c.Category)
                .Include(e => e.Inventories)
                    .ThenInclude(i => i.UnitOfMeasuring)
                .Include(e => e.Visitors)
                    .ThenInclude(v => v.User)
                        .ThenInclude(u => u.Photo)
                .FirstOrDefault(x => x.Id == eventId));

        public IEnumerable<EventDTO> GetAll(EventFilterViewModel model, out int count)
        {
            var events = _context.Events
                .Include(e => e.Photo)
                .Include(e => e.Owners)
                    .ThenInclude(o => o.User)
                        .ThenInclude(c => c.Photo)
                .Include(e => e.City)
                    .ThenInclude(c => c.Country)
                .Include(e => e.Categories)
                    .ThenInclude(c => c.Category)
                .Include(e => e.Visitors)
                .AsNoTracking()
                .AsQueryable();

            events = !string.IsNullOrEmpty(model.KeyWord)
                ? events.Where(x => x.Title.Contains(model.KeyWord)
                    || x.Description.Contains(model.KeyWord)
                    || x.City.Name.Contains(model.KeyWord)
                    || x.City.Country.Name.Contains(model.KeyWord))
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

            return _mapper.Map<IEnumerable<EventDTO>>(result);
        }

        public IEnumerable<EventDTO> FutureEventsByUserId(Guid userId, PaginationViewModel paginationViewModel)
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

        public IEnumerable<EventDTO> PastEventsByUserId(Guid userId, PaginationViewModel paginationViewModel)
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

        public IEnumerable<EventDTO> VisitedEventsByUserId(Guid userId, PaginationViewModel paginationViewModel)
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

        public IEnumerable<EventDTO> EventsToGoByUserId(Guid userId, PaginationViewModel paginationViewModel)
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

        public IEnumerable<EventDTO> GetEvents(List<Guid> eventIds, PaginationViewModel paginationViewModel)
        {
            var events = _context.Events
                .Include(e => e.Photo)
                .Include(e => e.Owners)
                    .ThenInclude(o => o.User)
                        .ThenInclude(c => c.Photo)
                .Include(e => e.City)
                    .ThenInclude(c => c.Country)
                .Include(e => e.Categories)
                    .ThenInclude(c => c.Category)
                .Include(e => e.Visitors)
                .Where(x => eventIds.Contains(x.Id))
                .AsNoTracking()
                .AsQueryable();

            paginationViewModel.Count = events.Count();

            events = events.Skip((paginationViewModel.Page - 1) * paginationViewModel.PageSize)
                .Take(paginationViewModel.PageSize);

            return _mapper.Map<IEnumerable<EventDTO>>(events);
        }

        public async Task SetRate(Guid userId, Guid eventId, byte rate)
        {
            var ev = _context.Events
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

            await _context.SaveChangesAsync();
        }

        public byte GetRateFromUser(Guid userId, Guid eventId)
        {
            return _context.Rates
                .FirstOrDefault(r => r.UserFromId == userId && r.EventId == eventId)
                ?.Score ?? 0;
        }

        public double GetRate(Guid eventId)
        {
            try
            {
                return _context.Rates
                    .Where(r => r.EventId == eventId)
                    .Average(r => r.Score);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool UserIsVisitor(Guid userId, Guid eventId) =>
                 _context.Events
                    .Include(e => e.Visitors)
                    .FirstOrDefault(e => e.Id == eventId)?.Visitors
                    ?.FirstOrDefault(v => v.UserId == userId) != null;

        public bool Exists(Guid id) => _context.Events.Find(id) != null;
    }
}
