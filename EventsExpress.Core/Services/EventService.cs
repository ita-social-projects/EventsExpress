using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.BaseService;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services
{
    public class EventService : BaseService<Event>, IEventService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly IMediator _mediator;
        private readonly IEventScheduleService _eventScheduleService;

        public EventService(
            AppDbContext context,
            IMapper mapper,
            IMediator mediator,
            IPhotoService photoSrv,
            IEventScheduleService eventScheduleService)
            : base(context)
        {
            _context = context;
            _mapper = mapper;
            _photoService = photoSrv;
            _mediator = mediator;
            _eventScheduleService = eventScheduleService;
        }

        public async Task<OperationResult> AddUserToEvent(Guid userId, Guid eventId)
        {
            var ev = Get("Visitors").FirstOrDefault(e => e.Id == eventId);
            if (ev == null)
            {
                return new OperationResult(false, "Event not found!", "eventId");
            }

            if (ev.MaxParticipants <= ev.Visitors.Count)
            {
                return new OperationResult(false, "To much participants!", " ");
            }

            var us = _context.Users.Find(userId);
            if (us == null)
            {
                return new OperationResult(false, "User not found!", "userID");
            }

            if (ev.Visitors == null)
            {
                ev.Visitors = new List<UserEvent>();
            }

            if (ev.IsPublic)
            {
                ev.Visitors.Add(new UserEvent { EventId = eventId, UserId = userId, UserStatusEvent = UserStatusEvent.Approved });
            }
            else
            {
                ev.Visitors.Add(new UserEvent { EventId = eventId, UserId = userId, UserStatusEvent = UserStatusEvent.Pending });
            }

            await _context.SaveChangesAsync();

            return new OperationResult(true);
        }

        public async Task<OperationResult> ChangeVisitorStatus(Guid userId, Guid eventId, UserStatusEvent status)
        {
            var userEvent = _context.UserEvent
                .Where(x => x.EventId == eventId && x.UserId == userId)
                .FirstOrDefault();

            userEvent.UserStatusEvent = status;
            await _mediator.Publish(new ParticipationMessage(userEvent.UserId, userEvent.EventId, status));

            _context.UserEvent.Update(userEvent);
            await _context.SaveChangesAsync();
            return new OperationResult(true);
        }

        public async Task<OperationResult> DeleteUserFromEvent(Guid userId, Guid eventId)
        {
            var ev = Get("Visitors").FirstOrDefault(e => e.Id == eventId);
            if (ev == null)
            {
                return new OperationResult(false, "Event not found!", "eventId");
            }

            var v = ev.Visitors?.FirstOrDefault(x => x.UserId == userId);
            if (v != null)
            {
                ev.Visitors.Remove(v);
                await _context.SaveChangesAsync();

                return new OperationResult(true);
            }

            return new OperationResult(false, "Visitor not found!", "visitorId");
        }

        public async Task<OperationResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return new OperationResult(false, "Id field is '0'", string.Empty);
            }

            var ev = Get(id);
            if (ev == null)
            {
                return new OperationResult(false, "Not found", string.Empty);
            }

            var result = Delete(ev);
            await _context.SaveChangesAsync();

            if (result != null)
            {
                return new OperationResult(true);
            }

            return new OperationResult(false, "Error!", string.Empty);
        }

        public async Task<OperationResult> BlockEvent(Guid eID)
        {
            var uEvent = Get(eID);
            if (uEvent == null)
            {
                return new OperationResult(false, "Invalid event id", "eventId");
            }

            uEvent.IsBlocked = true;

            await _context.SaveChangesAsync();
            await _mediator.Publish(new BlockedEventMessage(uEvent.OwnerId, uEvent.Id));

            return new OperationResult(true);
        }

        public async Task<OperationResult> UnblockEvent(Guid eId)
        {
            var uEvent = Get(eId);
            if (uEvent == null)
            {
                return new OperationResult(false, "Invalid event Id", "eventId");
            }

            uEvent.IsBlocked = false;

            await _context.SaveChangesAsync();
            await _mediator.Publish(new UnblockedEventMessage(uEvent.OwnerId, uEvent.Id));

            return new OperationResult(true);
        }

        public async Task<OperationResult> Create(EventDTO eventDTO)
        {
            eventDTO.DateFrom = (eventDTO.DateFrom == DateTime.MinValue) ? DateTime.Today : eventDTO.DateFrom;
            eventDTO.DateTo = (eventDTO.DateTo < eventDTO.DateFrom) ? eventDTO.DateFrom : eventDTO.DateTo;

            var ev = _mapper.Map<EventDTO, Event>(eventDTO);

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
                    return new OperationResult(false, "Invalid file", string.Empty);
                }
            }

            var eventCategories = eventDTO.Categories?
                .Select(x => new EventCategory { Event = ev, CategoryId = x.Id })
                .ToList();
            ev.Categories = eventCategories;
            ev.CreatedBy = ev.OwnerId;
            ev.ModifiedBy = ev.OwnerId;

            try
            {
                var result = Insert(ev);

                await _context.SaveChangesAsync();

                eventDTO.Id = result.Id;

                if (eventDTO.IsReccurent)
                {
                    await _eventScheduleService.Create(_mapper.Map<EventScheduleDTO>(eventDTO));
                }

                await _mediator.Publish(new EventCreatedMessage(eventDTO));
                return new OperationResult(true, "Create new Event", result.Id.ToString());
            }
            catch (Exception ex)
            {
                return new OperationResult(false, ex.Message, string.Empty);
            }
        }

        public async Task<OperationResult> CreateNextEvent(Guid eventId)
        {
            var eventDTO = EventById(eventId);
            var eventScheduleDTO = _eventScheduleService.EventScheduleByEventId(eventId);

            var ticksDiff = eventDTO.DateTo.Ticks - eventDTO.DateFrom.Ticks;
            eventDTO.Id = Guid.Empty;
            eventDTO.IsReccurent = false;
            eventDTO.DateFrom = eventScheduleDTO.NextRun;
            eventDTO.DateTo = eventDTO.DateFrom.AddTicks(ticksDiff);

            eventScheduleDTO.ModifiedBy = eventDTO.OwnerId;
            eventScheduleDTO.LastRun = eventDTO.DateTo;
            eventScheduleDTO.NextRun = DateTimeExtensions
                .AddDateUnit(eventScheduleDTO.Periodicity, eventScheduleDTO.Frequency, eventDTO.DateTo);

            try
            {
                var createResult = await Create(eventDTO);
                await _eventScheduleService.Edit(eventScheduleDTO);
                return new OperationResult(true, "new eventId", createResult.Property);
            }
            catch (Exception ex)
            {
                return new OperationResult(false, ex.Message, string.Empty);
            }
        }

        public async Task<OperationResult> Edit(EventDTO e)
        {
            var ev = Get("Photo,Categories.Category").FirstOrDefault(x => x.Id == e.Id);
            ev.Title = e.Title;
            ev.MaxParticipants = e.MaxParticipants;
            ev.Description = e.Description;
            ev.DateFrom = e.DateFrom;
            ev.DateTo = e.DateTo;
            ev.CityId = e.CityId;
            ev.ModifiedBy = ev.OwnerId;
            ev.ModifiedDateTime = DateTime.UtcNow;
            ev.IsPublic = e.IsPublic;

            if (e.Photo != null && ev.Photo != null)
            {
                await _photoService.Delete(ev.Photo.Id);
                try
                {
                    ev.Photo = await _photoService.AddPhoto(e.Photo);
                }
                catch
                {
                    return new OperationResult(false, "Invalid file", string.Empty);
                }
            }

            var eventCategories = e.Categories?.Select(x => new EventCategory { Event = ev, CategoryId = x.Id })
                .ToList();

            ev.Categories = eventCategories;

            await _context.SaveChangesAsync();
            return new OperationResult(true, "Edit event", ev.Id.ToString());
        }

        public async Task<OperationResult> EditNextEvent(EventDTO eventDTO)
        {
            var eventScheduleDTO = _eventScheduleService.EventScheduleByEventId(eventDTO.Id);

            eventScheduleDTO.ModifiedBy = eventDTO.OwnerId;
            eventScheduleDTO.LastRun = eventDTO.DateTo;
            eventScheduleDTO.NextRun = DateTimeExtensions
                .AddDateUnit(eventScheduleDTO.Periodicity, eventScheduleDTO.Frequency, eventDTO.DateTo);

            eventDTO.IsReccurent = false;
            eventDTO.Id = Guid.Empty;

            try
            {
                var createResult = await Create(eventDTO);
                await _eventScheduleService.Edit(eventScheduleDTO);
                return new OperationResult(true, "new eventId", createResult.Property);
            }
            catch (Exception ex)
            {
                return new OperationResult(false, ex.Message, string.Empty);
            }
        }

        public EventDTO EventById(Guid eventId) =>
            _mapper.Map<EventDTO>(
                 Get("Photo,Owner.Photo,City.Country,Categories.Category,Visitors.User.Photo,Inventories.UnitOfMeasuring")
                .FirstOrDefault(x => x.Id == eventId));

        public IEnumerable<EventDTO> GetAll(EventFilterViewModel model, out int count)
        {
            var events = Get("Photo,Owner.Photo,City.Country,Categories.Category,Visitors");

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
                .AsNoTracking()
                .ToList();

            return _mapper.Map<IEnumerable<EventDTO>>(result);
        }

        public IEnumerable<EventDTO> FutureEventsByUserId(Guid userId, PaginationViewModel paginationViewModel)
        {
            var ev = Get("Photo,Owner.Photo,City.Country,Categories.Category,Visitors.User.Photo")
                .Where(e => e.OwnerId == userId && e.DateFrom >= DateTime.Today)
                .OrderBy(e => e.DateFrom)
                .AsEnumerable();

            paginationViewModel.Count = ev.Count();
            return _mapper.Map<IEnumerable<EventDTO>>(ev.Skip((paginationViewModel.Page - 1) * paginationViewModel.PageSize).Take(paginationViewModel.PageSize));
        }

        public IEnumerable<EventDTO> PastEventsByUserId(Guid userId, PaginationViewModel paginationViewModel)
        {
            var ev = Get("Photo,Owner.Photo,City.Country,Categories.Category,Visitors.User.Photo")
                 .Where(e => e.OwnerId == userId && e.DateFrom < DateTime.Today)
                 .OrderBy(e => e.DateFrom)
                 .AsEnumerable();

            paginationViewModel.Count = ev.Count();
            return _mapper.Map<IEnumerable<EventDTO>>(ev.Skip((paginationViewModel.Page - 1) * paginationViewModel.PageSize).Take(paginationViewModel.PageSize));
        }

        public IEnumerable<EventDTO> VisitedEventsByUserId(Guid userId, PaginationViewModel paginationViewModel)
        {
            var ev = Get("Photo,Owner.Photo,City.Country,Categories.Category,Visitors.User.Photo")
                .Where(e => e.Visitors.Any(x => x.UserId == userId) && e.DateFrom < DateTime.Today)
                .OrderBy(e => e.DateFrom)
                .AsEnumerable();

            paginationViewModel.Count = ev.Count();
            return _mapper.Map<IEnumerable<EventDTO>>(ev.Skip((paginationViewModel.Page - 1) * paginationViewModel.PageSize).Take(paginationViewModel.PageSize));
        }

        public IEnumerable<EventDTO> EventsToGoByUserId(Guid userId, PaginationViewModel paginationViewModel)
        {
            var ev = Get("Photo,Owner.Photo,City.Country,Categories.Category,Visitors.User.Photo")
               .Where(e => e.Visitors.Where(x => x.UserId == userId).FirstOrDefault().UserId == userId).Where(x => x.DateTo > DateTime.UtcNow).AsEnumerable();

            paginationViewModel.Count = ev.Count();
            return _mapper.Map<IEnumerable<EventDTO>>(ev.Skip((paginationViewModel.Page - 1) * paginationViewModel.PageSize).Take(paginationViewModel.PageSize));
        }

        public IEnumerable<EventDTO> GetEvents(List<Guid> eventIds, PaginationViewModel paginationViewModel)
        {
            var events = Get("Photo,Owner.Photo,City.Country,Categories.Category,Visitors")
                .Where(x => eventIds.Contains(x.Id))
                .AsNoTracking()
                .ToList();

            paginationViewModel.Count = events.Count();

            return _mapper.Map<IEnumerable<EventDTO>>(
                events.Skip((paginationViewModel.Page - 1) * paginationViewModel.PageSize)
                .Take(paginationViewModel.PageSize));
        }

        public async Task<OperationResult> SetRate(Guid userId, Guid eventId, byte rate)
        {
            try
            {
                var ev = Get("Rates").FirstOrDefault(e => e.Id == eventId);
                ev.Rates = ev.Rates ?? new List<Rate>();

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
                return new OperationResult(true);
            }
            catch (Exception e)
            {
                return new OperationResult(false, e.Message, string.Empty);
            }
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
                    .AsNoTracking()
                    .ToList()
                    .Average(r => r.Score);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool UserIsVisitor(Guid userId, Guid eventId) =>
                 Get("Visitors")
                .FirstOrDefault(e => e.Id == eventId)?.Visitors
                ?.FirstOrDefault(v => v.UserId == userId) != null;

        public bool Exists(Guid id) => Get(id) != null;
    }
}
