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
using EventsExpress.Db.Bridge;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace EventsExpress.Core.Services
{
    public class EventService : BaseService<Event>, IEventService
    {
        private readonly IPhotoService _photoService;
        private readonly ILocationService _locationService;
        private readonly IMediator _mediator;
        private readonly IEventScheduleService _eventScheduleService;
        private readonly IValidator<Event> _validator;
        private readonly ISecurityContext _securityContextService;

        public EventService(
            AppDbContext context,
            IMapper mapper,
            IMediator mediator,
            IPhotoService photoService,
            ILocationService locationService,
            IEventScheduleService eventScheduleService,
            IValidator<Event> validator,
            ISecurityContext securityContextService)
            : base(context, mapper)
        {
            _photoService = photoService;
            _locationService = locationService;
            _mediator = mediator;
            _eventScheduleService = eventScheduleService;
            _validator = validator;
            _securityContextService = securityContextService;
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
                throw new EventsExpressException("Too much participants!");
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
                UserStatusEvent = ev.IsPublic.Value ? UserStatusEvent.Approved : UserStatusEvent.Pending,
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
            ev.Owners = new List<EventOwner>
            {
                new EventOwner
                {
                    UserId = CurrentUserId(),
                    EventId = ev.Id,
                },
            };

            var result = Insert(ev);
            Context.SaveChanges();

            return result.Id;
        }

        public async Task<Guid> Create(EventDto eventDTO)
        {
            eventDTO.DateFrom = (eventDTO.DateFrom == DateTime.MinValue) ? DateTime.Today : eventDTO.DateFrom;
            eventDTO.DateTo = (eventDTO.DateTo < eventDTO.DateFrom) ? eventDTO.DateFrom : eventDTO.DateTo;

            var locationDTO = Mapper.Map<EventDto, LocationDto>(eventDTO);
            var locationId = await _locationService.AddLocationToEvent(locationDTO);

            var ev = Mapper.Map<EventDto, Event>(eventDTO);
            ev.EventLocationId = locationId;

            ev.StatusHistory = new List<EventStatusHistory>
            {
                new EventStatusHistory
                {
                    EventStatus = EventStatus.Active,
                    CreatedOn = DateTime.UtcNow,
                    UserId = CurrentUserId(),
                },
            };

            ev.Owners = new List<EventOwner>
            {
                new EventOwner
                {
                    UserId = CurrentUserId(),
                    EventId = eventDTO.Id,
                },
            };

            var eventCategories = eventDTO.Categories?
                .Select(x => new EventCategory { Event = ev, CategoryId = x.Id })
                .ToList();
            ev.Categories = eventCategories;

            var result = Insert(ev);

            eventDTO.Id = result.Id;

            await Context.SaveChangesAsync();
            await _mediator.Publish(new EventCreatedMessage(eventDTO));

            await _photoService.ChangeTempToImagePhoto(eventDTO.Id);

            return result.Id;
        }

        public async Task<Guid> CreateNextEvent(Guid eventId)
        {
            var eventDTO = EventById(eventId);

            var eventScheduleDTO = _eventScheduleService.EventScheduleByEventId(eventId);

            var ticksDiff = eventDTO.DateTo.Value.Ticks - eventDTO.DateFrom.Value.Ticks;
            eventDTO.Id = Guid.Empty;
            eventDTO.Owners = null;
            eventDTO.Inventories = null;
            eventDTO.IsReccurent = false;
            eventDTO.DateFrom = eventScheduleDTO.NextRun;
            eventDTO.DateTo = eventDTO.DateFrom.Value.AddTicks(ticksDiff);
            eventScheduleDTO.LastRun = eventDTO.DateTo.Value;
            eventScheduleDTO.NextRun = DateTimeExtensions
                .AddDateUnit(eventScheduleDTO.Periodicity, eventScheduleDTO.Frequency, eventDTO.DateTo.Value);

            await _eventScheduleService.Edit(eventScheduleDTO);

            var createResult = await Create(eventDTO);

            return createResult;
        }

        public async Task<Guid> Edit(EventDto e)
        {
            var ev = Context.Events
                .Include(e => e.EventLocation)
                .Include(e => e.Categories)
                    .ThenInclude(c => c.Category)
                .Include(e => e.EventSchedule)
                .FirstOrDefault(x => x.Id == e.Id);

            if (e.OnlineMeeting != null || e.Point != null)
            {
                var locationDTO = Mapper.Map<EventDto, LocationDto>(e);
                var locationId = await _locationService.AddLocationToEvent(locationDTO);
                ev.EventLocationId = locationId;
            }

            if (e.IsReccurent)
            {
                if (e.EventStatus == EventStatus.Draft)
                {
                    if (ev.EventSchedule == null)
                    {
                        await _eventScheduleService.Create(Mapper.Map<EventScheduleDto>(e));
                    }
                    else
                    {
                        var eventScheduleDTO = Mapper.Map<EventScheduleDto>(e);
                        eventScheduleDTO.Id = ev.EventSchedule.Id;
                        await _eventScheduleService.Edit(eventScheduleDTO);
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

            ev.Title = e.Title;
            ev.MaxParticipants = e.MaxParticipants;
            ev.Description = e.Description;
            ev.DateFrom = e.DateFrom;
            ev.DateTo = e.DateTo;
            ev.IsPublic = e.IsPublic;

            var eventCategories = e.Categories?.Select(x => new EventCategory { Event = ev, CategoryId = x.Id })
                .ToList();

            ev.Categories = eventCategories;
            await Context.SaveChangesAsync();

            await _photoService.ChangeTempToImagePhoto(e.Id);

            return ev.Id;
        }

        public async Task<Guid> Publish(Guid eventId)
        {
            var ev = Context.Events
               .Include(e => e.EventLocation)
               .Include(e => e.StatusHistory)
               .Include(e => e.Categories)
                   .ThenInclude(c => c.Category)
               .FirstOrDefault(x => x.Id == eventId);

            if (ev == null)
            {
                throw new EventsExpressException("Not found");
            }

            Dictionary<string, string> exept = new Dictionary<string, string>();
            var result = _validator.Validate(ev);

            if (result.IsValid)
            {
                ev.StatusHistory.Add(
                    new EventStatusHistory
                    {
                        EventStatus = EventStatus.Active,
                        CreatedOn = DateTime.UtcNow,
                        UserId = CurrentUserId(),
                    });
                await Context.SaveChangesAsync();
                EventDto dtos = Mapper.Map<Event, EventDto>(ev);
                await _mediator.Publish(new EventCreatedMessage(dtos));
                return ev.Id;
            }
            else
            {
                var p = result.Errors.Select(e => new KeyValuePair<string, string>(e.PropertyName, e.ErrorMessage));
                foreach (var x in p)
                {
                    exept.Add(x.Key, x.Value);
                }

                throw new EventsExpressException("validation failed", exept);
            }
        }

        public async Task<Guid> EditNextEvent(EventDto eventDTO)
        {
            var eventScheduleDTO = _eventScheduleService.EventScheduleByEventId(eventDTO.Id);
            eventScheduleDTO.LastRun = eventDTO.DateTo.Value;
            eventScheduleDTO.NextRun = DateTimeExtensions
                .AddDateUnit(eventScheduleDTO.Periodicity, eventScheduleDTO.Frequency, eventDTO.DateTo.Value);

            await _eventScheduleService.Edit(eventScheduleDTO);

            eventDTO.IsReccurent = false;
            eventDTO.Id = Guid.Empty;

            var createResult = await Create(eventDTO);

            return createResult;
        }

        public EventDto EventById(Guid eventId)
        {
            var res = Mapper.Map<EventDto>(
                Context.Events
                .Include(e => e.EventLocation)
                .Include(e => e.Owners)
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
                .Include(e => e.EventSchedule)
                .FirstOrDefault(x => x.Id == eventId));

            return res;
        }

        public IEnumerable<EventDto> GetAll(EventFilterViewModel model, out int count)
        {
            var events = Context.Events
                .Include(e => e.EventLocation)
                .Include(e => e.StatusHistory)
                .Include(e => e.Owners)
                    .ThenInclude(o => o.User)
                .Include(e => e.Categories)
                    .ThenInclude(c => c.Category)
                .Include(e => e.Visitors)
                    .ThenInclude(v => v.User)
                        .ThenInclude(u => u.Relationships)
                .Include(e => e.StatusHistory)
                .AsNoTracking()
                .AsQueryable();
            events = events.Where(x => x.StatusHistory.OrderBy(h => h.CreatedOn).Last().EventStatus != EventStatus.Draft);

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
                ? events.Where(x => (x.EventLocation.Point.Distance(new Point((double)model.X, (double)model.Y) { SRID = 4326 }) / 1000) - (double)model.Radius <= 0)
                : events;

            events = (model.Statuses != null)
            ? events.Where(e => model.Statuses.Contains(e.StatusHistory
               .OrderByDescending(n => n.CreatedOn)
               .FirstOrDefault()
               .EventStatus))
            : events;

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

            var result = events
                .OrderBy(x => x.DateFrom)
                .Skip((model.Page - 1) * model.PageSize)
                .Take(model.PageSize)
                .ToList();

            return Mapper.Map<IEnumerable<EventDto>>(result);
        }

        public IEnumerable<EventDto> GetAllDraftEvents(int page, int pageSize, out int count)
        {
            var events = Context.Events
                .Include(e => e.EventLocation)
                .Include(e => e.StatusHistory)
                .Include(e => e.Owners)
                    .ThenInclude(o => o.User)
                .Include(e => e.Categories)
                    .ThenInclude(c => c.Category)
                .Include(e => e.Visitors)
                .AsNoTracking()
                .AsQueryable();
            events = events.Where(x => x.StatusHistory.OrderBy(h => h.CreatedOn).Last().EventStatus == EventStatus.Draft);
            events = events.Where(x => x.Owners.Any(o => o.UserId == CurrentUserId()));
            count = events.Count();
            var result = events.OrderBy(x => x.DateFrom).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return Mapper.Map<IEnumerable<Event>, IEnumerable<EventDto>>(result);
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

            var events = this.GetAll(filter, out int count);

            paginationViewModel.Count = count;

            return events;
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
                .Include(e => e.EventLocation)
                .Include(e => e.Owners)
                    .ThenInclude(o => o.User)
                .Include(e => e.Categories)
                    .ThenInclude(c => c.Category)
                .Include(e => e.Visitors)
                .Include(e => e.StatusHistory)
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

        private Guid CurrentUserId() =>
           _securityContextService.GetCurrentUserId();
    }
}
