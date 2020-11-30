using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Extensions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Core.Notifications;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using EventsExpress.Db.IRepo;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly IMediator _mediator;
        private readonly IEventScheduleService _eventScheduleService;

        public EventService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IMediator mediator,
            IPhotoService photoSrv,
            IEventScheduleService eventScheduleService)
        {
            _db = unitOfWork;
            _mapper = mapper;
            _photoService = photoSrv;
            _mediator = mediator;
            _eventScheduleService = eventScheduleService;
        }

        public async Task AddUserToEvent(Guid userId, Guid eventId)
        {
            var ev = _db.EventRepository.Get("Visitors").FirstOrDefault(e => e.Id == eventId);
            if (ev == null)
            {
                throw new EventsExpressException("Event not found!");
            }

            if (ev.MaxParticipants <= ev.Visitors.Count)
            {
                throw new EventsExpressException("To much participants!");
            }

            var us = _db.UserRepository.Get(userId);
            if (us == null)
            {
                throw new EventsExpressException("User not found!");
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

            await _db.SaveAsync();
        }

        public async Task ChangeVisitorStatus(Guid userId, Guid eventId, UserStatusEvent status)
        {
            var userEvent = _db.UserEventRepository
                .Get(string.Empty)
                .Where(x => x.EventId == eventId && x.UserId == userId)
                .FirstOrDefault();

            userEvent.UserStatusEvent = status;
            await _mediator.Publish(new ParticipationMessage(userEvent.UserId, userEvent.EventId, status));

            _db.UserEventRepository.Update(userEvent);
            await _db.SaveAsync();
        }

        public async Task DeleteUserFromEvent(Guid userId, Guid eventId)
        {
            var ev = _db.EventRepository.Get("Visitors").FirstOrDefault(e => e.Id == eventId);
            if (ev == null)
            {
                throw new EventsExpressException("Event not found!");
            }

            var v = ev.Visitors?.FirstOrDefault(x => x.UserId == userId);
            if (v != null)
            {
                ev.Visitors.Remove(v);
                await _db.SaveAsync();
            }

            throw new EventsExpressException("Visitor not found!");
        }

        public async Task Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new EventsExpressException("Id field is '0'");
            }

            var ev = _db.EventRepository.Get(id);
            if (ev == null)
            {
                throw new EventsExpressException("Not found");
            }

            var result = _db.EventRepository.Delete(ev);
            await _db.SaveAsync();
        }

        public async Task BlockEvent(Guid eID)
        {
            var uEvent = _db.EventRepository.Get(eID);
            if (uEvent == null)
            {
                throw new EventsExpressException("Invalid event id");
            }

            uEvent.IsBlocked = true;

            await _db.SaveAsync();
            await _mediator.Publish(new BlockedEventMessage(uEvent.OwnerId, uEvent.Id));
        }

        public async Task UnblockEvent(Guid eId)
        {
            var uEvent = _db.EventRepository.Get(eId);
            if (uEvent == null)
            {
                throw new EventsExpressException("Invalid event Id");
            }

            uEvent.IsBlocked = false;

            await _db.SaveAsync();
            await _mediator.Publish(new UnblockedEventMessage(uEvent.OwnerId, uEvent.Id));
        }

        public async Task<Guid> Create(EventDTO eventDTO)
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
                    throw new EventsExpressException("Invalid file");
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
                var result = _db.EventRepository.Insert(ev);

                await _db.SaveAsync();

                eventDTO.Id = result.Id;

                if (eventDTO.IsReccurent)
                {
                    await _eventScheduleService.Create(_mapper.Map<EventScheduleDTO>(eventDTO));
                }

                await _mediator.Publish(new EventCreatedMessage(eventDTO));
                return result.Id;
            }
            catch (Exception ex)
            {
                throw new EventsExpressException(ex.Message);
            }
        }

        public async Task<Guid> CreateNextEvent(Guid eventId)
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
                return createResult;
            }
            catch (Exception ex)
            {
                throw new EventsExpressException(ex.Message);
            }
        }

        public async Task<Guid> Edit(EventDTO e)
        {
            var ev = _db.EventRepository.Get("Photo,Categories.Category").FirstOrDefault(x => x.Id == e.Id);
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
                    throw new EventsExpressException("Invalid file");
                }
            }

            var eventCategories = e.Categories?.Select(x => new EventCategory { Event = ev, CategoryId = x.Id })
                .ToList();

            ev.Categories = eventCategories;

            await _db.SaveAsync();
            return ev.Id;
        }

        public async Task<Guid> EditNextEvent(EventDTO eventDTO)
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
                return createResult;
            }
            catch (Exception ex)
            {
                throw new EventsExpressException(ex.Message);
            }
        }

        public EventDTO EventById(Guid eventId) =>
            _mapper.Map<EventDTO>(_db.EventRepository
                .Get("Photo,Owner.Photo,City.Country,Categories.Category,Visitors.User.Photo,Inventories.UnitOfMeasuring")
                .FirstOrDefault(x => x.Id == eventId));

        public IEnumerable<EventDTO> GetAll(EventFilterViewModel model, out int count)
        {
            var events = _db.EventRepository
                .Get("Photo,Owner.Photo,City.Country,Categories.Category,Visitors");

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
            var ev = _db.EventRepository.Get("Photo,Owner.Photo,City.Country,Categories.Category,Visitors.User.Photo")
                .Where(e => e.OwnerId == userId && e.DateFrom >= DateTime.Today)
                .OrderBy(e => e.DateFrom)
                .AsEnumerable();

            paginationViewModel.Count = ev.Count();
            return _mapper.Map<IEnumerable<EventDTO>>(ev.Skip((paginationViewModel.Page - 1) * paginationViewModel.PageSize).Take(paginationViewModel.PageSize));
        }

        public IEnumerable<EventDTO> PastEventsByUserId(Guid userId, PaginationViewModel paginationViewModel)
        {
            var ev = _db.EventRepository.Get("Photo,Owner.Photo,City.Country,Categories.Category,Visitors.User.Photo")
                 .Where(e => e.OwnerId == userId && e.DateFrom < DateTime.Today)
                 .OrderBy(e => e.DateFrom)
                 .AsEnumerable();

            paginationViewModel.Count = ev.Count();
            return _mapper.Map<IEnumerable<EventDTO>>(ev.Skip((paginationViewModel.Page - 1) * paginationViewModel.PageSize).Take(paginationViewModel.PageSize));
        }

        public IEnumerable<EventDTO> VisitedEventsByUserId(Guid userId, PaginationViewModel paginationViewModel)
        {
            var ev = _db.EventRepository.Get("Photo,Owner.Photo,City.Country,Categories.Category,Visitors.User.Photo")
                .Where(e => e.Visitors.Any(x => x.UserId == userId) && e.DateFrom < DateTime.Today)
                .OrderBy(e => e.DateFrom)
                .AsEnumerable();

            paginationViewModel.Count = ev.Count();
            return _mapper.Map<IEnumerable<EventDTO>>(ev.Skip((paginationViewModel.Page - 1) * paginationViewModel.PageSize).Take(paginationViewModel.PageSize));
        }

        public IEnumerable<EventDTO> EventsToGoByUserId(Guid userId, PaginationViewModel paginationViewModel)
        {
            var ev = _db.EventRepository.Get("Photo,Owner.Photo,City.Country,Categories.Category,Visitors.User.Photo")
               .Where(e => e.Visitors.Where(x => x.UserId == userId).FirstOrDefault().UserId == userId).Where(x => x.DateTo > DateTime.UtcNow).AsEnumerable();

            paginationViewModel.Count = ev.Count();
            return _mapper.Map<IEnumerable<EventDTO>>(ev.Skip((paginationViewModel.Page - 1) * paginationViewModel.PageSize).Take(paginationViewModel.PageSize));
        }

        public IEnumerable<EventDTO> GetEvents(List<Guid> eventIds, PaginationViewModel paginationViewModel)
        {
            var events = _db.EventRepository
                .Get("Photo,Owner.Photo,City.Country,Categories.Category,Visitors")
                .Where(x => eventIds.Contains(x.Id))
                .AsNoTracking()
                .ToList();

            paginationViewModel.Count = events.Count();

            return _mapper.Map<IEnumerable<EventDTO>>(
                events.Skip((paginationViewModel.Page - 1) * paginationViewModel.PageSize)
                .Take(paginationViewModel.PageSize));
        }

        public async Task SetRate(Guid userId, Guid eventId, byte rate)
        {
            try
            {
                var ev = _db.EventRepository.Get("Rates").FirstOrDefault(e => e.Id == eventId);
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

                await _db.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new EventsExpressException(ex.Message);
            }
        }

        public byte GetRateFromUser(Guid userId, Guid eventId)
        {
            return _db.RateRepository.Get()
                .FirstOrDefault(r => r.UserFromId == userId && r.EventId == eventId)
                ?.Score ?? 0;
        }

        public double GetRate(Guid eventId)
        {
            try
            {
                return _db.RateRepository.Get()
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
            _db.EventRepository.Get("Visitors")
                .FirstOrDefault(e => e.Id == eventId)?.Visitors
                ?.FirstOrDefault(v => v.UserId == userId) != null;

        public bool Exists(Guid id) => _db.EventRepository.Get(id) != null;
    }
}
