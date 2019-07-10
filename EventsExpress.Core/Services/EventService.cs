using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsExpress.Core.Services
{
   public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly IHostingEnvironment _appEnvironment;
        public EventService(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext context, IHostingEnvironment hostingEnvironment) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
            _appEnvironment = hostingEnvironment;
        }
        public bool Exists(Guid id) => this._context.Events.Any(e => e.Id == id);

        public bool UserIsAuthorizedToEdit(Guid eventid, Guid userId) => this._context.Events.Any(p => p.Id == eventid && p.OwnerId == userId);

        public async Task<OperationResult> AddUserToEvent(Guid userId, Guid eventId)
        {
            var ev = _unitOfWork.EventRepository
               .Filter( filter: e => e.Id == eventId, includeProperties: "Visitors")
               .FirstOrDefault();

            if (ev == null)
            {
                return new OperationResult(false, "Event not found!", "eventId");
            }

            if (ev.Visitors == null)
            {
                ev.Visitors = new List<UserEvent>();
            }

            ev.Visitors.Add(new UserEvent { EventId = eventId, UserId = userId });
            await _unitOfWork.SaveAsync();

            return new OperationResult(true, "", "");
        }
        public async Task<OperationResult> Delete(Guid id)
        {
            if (id == null)
            {
                return new OperationResult(false, "Id field is '0'", "");
            }
            Event ev = _unitOfWork.EventRepository.Get(id);
            if (ev == null)
            {
                return new OperationResult(false, "Not found", "");
            }

            var result = _unitOfWork.EventRepository.Delete(ev);
            await _unitOfWork.SaveAsync();

            if (result == null)
            {
                return new OperationResult(true, "Ok", "");
            }
            else {
                return new OperationResult(false, "Error!", "");
            }
        }
        public IEnumerable<EventDTO> UpcomingThreeEvents()
        {
            var ev = this._context
                .Events.Where(e => e.DateTo <= DateTime.UtcNow)
                .OrderBy(e => e.DateFrom)
                .ToList();
            return _mapper.Map<IEnumerable<EventDTO>>(ev);
        }
        public async Task<OperationResult> Create(EventDTO e)
        {
            Event evnt = new Event
            {
                Title = e.Title,
                Description = e.Description,
                DateFrom = e.DateFrom,
                DateTo = e.DateTo,
            };
            //add location
           
            City city = _unitOfWork.CityRepository.Get(e.City.Id);
            evnt.City = city;
            evnt.Owner = _unitOfWork.UserRepository.Get(e.UserId);


            evnt = _unitOfWork.EventRepository.Insert(evnt);

            if (e.Photo != null)
            {
                string path = "/files/" + e.Photo.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await e.Photo.CopyToAsync(fileStream);
                }

                Photo photo = _unitOfWork.PhotoRepository.Insert(new Photo { Path = path });
                evnt.Photo = photo;
                _unitOfWork.EventRepository.Update(evnt);
            }
            List<EventCategory> eventCategories = new List<EventCategory>();
         
            foreach (var item in e.Categories)
            {
                eventCategories.Add(new EventCategory
                {
                    Event = evnt,
                    Category = _unitOfWork.CategoryRepository.GetByTitle(item)
                });
            }
            evnt.Categories = eventCategories;
            await _unitOfWork.SaveAsync();
            return new OperationResult(true, "Ok", "");
        }
        public async Task<OperationResult> Edit(EventDTO e)
        {
            var evnt = _unitOfWork.EventRepository.Get(e.EventId);
            evnt.Title = e.Title;
            evnt.Description = e.Description;
            evnt.DateFrom = e.DateFrom;
            evnt.DateTo = e.DateTo;

            City city = _unitOfWork.CityRepository.Get(e.City.Id);
            evnt.City = city;


            if (e.Photo != null)
            {
                string path = "/files/" + e.Photo.FileName;
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await e.Photo.CopyToAsync(fileStream);
                }

                Photo photo = _unitOfWork.PhotoRepository.Insert(new Photo { Path = path });
                evnt.Photo = photo;
                _unitOfWork.EventRepository.Update(evnt);
            }
            List<EventCategory> eventCategories = new List<EventCategory>();

            foreach (var item in e.Categories)
            {
                eventCategories.Add(new EventCategory
                {
                    Event = evnt,
                    Category = _unitOfWork.CategoryRepository.GetByTitle(item)
                });
            }
            evnt.Categories = eventCategories;
            await _unitOfWork.SaveAsync();
            return new OperationResult(true, "Ok", "");
        }
        public IEnumerable<EventDTO> Events()
        {
            var events = _unitOfWork.EventRepository.Get().ToList();

            return _mapper.Map<IEnumerable<Event>, IEnumerable<EventDTO>>(events);
        }
        public EventDTO EventById(Guid eventId)
        {
            var evv = _unitOfWork.EventRepository.Get(eventId);
            return _mapper.Map<EventDTO>(evv);
        }
        public IEnumerable<EventDTO> EventsByUserId(Guid userId)
        {  
            var evv = _unitOfWork.EventRepository.Filter(filter: e => e.OwnerId == userId);
            return _mapper.Map<IEnumerable<EventDTO>>(evv);

        }
        public EventDTO Details(Guid event_id)
        {
            var ev = _unitOfWork.EventRepository.Filter(filter: e => e.Id == event_id, includeProperties: "Title,Description,DateFrom,DateTo,City,Photo,EventCategory,Visitors").FirstOrDefault();
            List<string> Categories = new List<string>();
            foreach (var x in _unitOfWork.CategoryRepository.EventCategories(event_id))
            {
                Categories.Add(x.Name);
            }
            EventDTO eventDTO = _mapper.Map<Event, EventDTO>(ev);
                
            eventDTO.Visitors = ev.Visitors
               .Select(x => x.User.Id)
               .ToList();

            return eventDTO;
        }
    }
}

