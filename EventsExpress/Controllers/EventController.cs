using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.DTO;
using EventsExpress.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public EventController(
            IEventService eventService,
            IAuthService authSrv,
            IMapper mapper)
        {
            _eventService = eventService;
            _authService = authSrv;
            _mapper = mapper;
        }

        /// <summary>
        /// This method is for edit and create events.
        /// </summary>
        /// <param name="model">Required.</param>
        /// <response code="200">Edit/Create event proces success.</response>
        /// <response code="400">If Edit/Create process failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> Edit([FromForm] EventDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = model.Id == Guid.Empty
                ? await _eventService.Create(_mapper.Map<EventDTO>(model))
                : await _eventService.Edit(_mapper.Map<EventDTO>(model));
            if (result.Successed)
            {
                return Ok(result.Property);
            }

            return BadRequest(result.Message);
        }

        /// <summary>
        /// This method have to return event.
        /// </summary>
        /// <param name="id">Required.</param>
        /// <returns>Event.</returns>
        /// <response code="200">Return UserInfo model.</response>
        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult Get(Guid id) =>
            Ok(_mapper.Map<EventDto>(_eventService.EventById(id)));

        /// <summary>
        /// This method have to return all events.
        /// </summary>
        /// <param name="filter">Required.</param>
        /// <returns>AllEvents.</returns>
        /// <response code="200">Return IEnumerable EventPreviewDto.</response>
        /// <response code="400">If return failed.</response>
        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult All([FromQuery] EventFilterViewModel filter)
        {
            filter.PageSize = 6;
            IEnumerable<EventPreviewDto> indexItems = _mapper.Map<IEnumerable<EventPreviewDto>>(
                _eventService.GetAll(filter, out int count));

            if (!User.IsInRole("Admin"))
            {
                indexItems = indexItems.Where(x => x.DateFrom >= DateTime.Today);
            }

            try
            {
                var viewModel = new IndexViewModel<EventPreviewDto>
                {
                    Items = indexItems,
                    PageViewModel = new PageViewModel(count, filter.Page, filter.PageSize),
                };
                return Ok(viewModel);
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// This method have to add user to category.
        /// </summary>
        /// <param name="userId">Required.</param>
        /// <param name="eventId">EventId.</param>
        /// <response code="200">Adding user from event proces success.</response>
        /// <response code="400">If adding user from event process failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> AddUserToEvent(Guid userId, Guid eventId)
        {
            var res = await _eventService.AddUserToEvent(userId, eventId);
            if (res.Successed)
            {
                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// This method have to add user to category.
        /// </summary>
        /// <param name="userId">Required.</param>
        /// <param name="eventId">EventId.</param>
        /// <param name="action">button press.</param>
        /// <response code="200">Approving user from event process success.</response>
        /// <response code="400">If aproving user from event process failed.</response>
        [HttpPost("[action]")]
        public async Task<ActionResult> ApproveUserToEvent(Guid userId, Guid eventId, bool action)
        {
            var res = await _eventService.ApproveUserToEvent(userId, eventId, action);
            if (res.Successed)
            {
                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// This method have to add user to category.
        /// </summary>
        /// <param name="userId">Required.</param>
        /// <param name="eventId">EventId.</param>
        /// <response code="200">Delete  user from event proces success.</response>
        /// <response code="400">If deleting user from event process failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteUserFromEvent(Guid userId, Guid eventId)
        {
            var res = await _eventService.DeleteUserFromEvent(userId, eventId);
            if (res.Successed)
            {
                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// This method is to block event.
        /// </summary>
        /// <param name="eventId">Required.</param>
        /// <response code="200">Block is succesful.</response>
        /// <response code="302">If user isn't admin.</response>
        /// <response code="400">Block process failed.</response>
        [HttpPost("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Block(Guid eventId)
        {
            var result = await _eventService.BlockEvent(eventId);
            if (!result.Successed)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }

        /// <summary>
        /// This method is to unblock event.
        /// </summary>
        /// <param name="eventId">Required.</param>
        /// <response code="200">Unblock is succesful.</response>
        /// <response code="400">Unblock process is failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> Unblock(Guid eventId)
        {
            var result = await _eventService.UnblockEvent(eventId);
            if (!result.Successed)
            {
                return BadRequest(result.Message);
            }

            return Ok();
        }

        /// <summary>
        /// This method id used to set rating to user.
        /// </summary>
        /// <param name="model">Required (type: RateDto).</param>
        /// <response code="200">Rating is setted successfully.</response>
        /// <response code="400">Setting rating is failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> SetRate(RateDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _eventService.SetRate(model.UserId, model.EventId, model.Rate);
            if (result.Successed)
            {
                return Ok();
            }

            return BadRequest();
        }

        /// <summary>
        /// This method gets current rate for event.
        /// </summary>
        /// <param name="eventId">Required (type: Guid).</param>
        /// <response code="200">Getting is successful.</response>
        /// <response code="400">Getting is failed.</response>
        [HttpGet("[action]/{eventId}")]
        public IActionResult GetCurrentRate(Guid eventId)
        {
            if (!_eventService.Exists(eventId))
            {
                return BadRequest("Invalid id");
            }

            var userId = _authService.GetCurrentUser(HttpContext.User).Id;

            return Ok(_eventService.GetRateFromUser(userId, eventId));
        }

        /// <summary>
        /// This method gets average rate for event.
        /// </summary>
        /// <param name="eventId">Reguired (type: Guid).</param>
        /// <returns>RateOfEvent.</returns>
        /// <response code="200">Getting is successful.</response>
        /// <response code="400">Getting is failed.</response>
        [HttpGet("[action]/{eventId}")]
        public IActionResult GetAverageRate(Guid eventId)
        {
            if (!_eventService.Exists(eventId))
            {
                return BadRequest("Invalid id");
            }

            return Ok(_eventService.GetRate(eventId));
        }

        /// <summary>
        /// This method gets future events for user profile.
        /// </summary>
        /// <param name="id">Reguired.</param>
        /// <param name="page">CountPages.</param>
        /// <response code="200">Getting is successful.</response>
        /// <response code="400">Getting is failed.</response>
        [HttpGet("[action]")]
        public IActionResult FutureEvents(Guid id, int page = 1)
        {
            var model = new PaginationViewModel
            {
                PageSize = 3,
                Page = page,
            };
            try
            {
                var viewModel = new IndexViewModel<EventPreviewDto>
                {
                    Items = _mapper.Map<IEnumerable<EventPreviewDto>>(_eventService.FutureEventsByUserId(id, model)),
                    PageViewModel = new PageViewModel(model.Count, model.Page, model.PageSize),
                };
                return Ok(viewModel);
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// This method gets finished events.
        /// </summary>
        /// <param name="id">Reguired.</param>
        /// <param name="page">CountPages.</param>
        /// <response code="200">Getting is successful.</response>
        /// <response code="400">Getting is failed.</response>
        [HttpGet("[action]")]
        public IActionResult PastEvents(Guid id, int page = 1)
        {
            var model = new PaginationViewModel
            {
                PageSize = 3,
                Page = page,
            };
            try
            {
                var viewModel = new IndexViewModel<EventPreviewDto>
                {
                    Items = _mapper.Map<IEnumerable<EventPreviewDto>>(_eventService.PastEventsByUserId(id, model)),
                    PageViewModel = new PageViewModel(model.Count, model.Page, model.PageSize),
                };
                return Ok(viewModel);
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// This method gets  events which have to visit.
        /// </summary>
        /// <param name="id">Reguired.</param>
        /// <param name="page">CountPages.</param>
        /// <returns>Events.</returns>
        /// <response code="200">Getting is successful.</response>
        /// <response code="400">Getting is failed.</response>
        [HttpGet("[action]")]
        public IActionResult EventsToGo(Guid id, int page = 1)
        {
            var model = new PaginationViewModel
            {
                PageSize = 3,
                Page = page,
            };
            try
            {
                var viewModel = new IndexViewModel<EventPreviewDto>
                {
                    Items = _mapper.Map<IEnumerable<EventPreviewDto>>(_eventService.EventsToGoByUserId(id, model)),
                    PageViewModel = new PageViewModel(model.Count, model.Page, model.PageSize),
                };
                return Ok(viewModel);
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// This method gets  events which have visited.
        /// </summary>
        /// <param name="id">Reguired.</param>
        /// <param name="page">CountPages.</param>
        /// <returns>Events.</returns>
        /// <response code="200">Getting is successful.</response>
        /// <response code="400">Getting is failed.</response>
        [HttpGet("[action]")]
        public IActionResult VisitedEvents(Guid id, int page = 1)
        {
            var model = new PaginationViewModel
            {
                PageSize = 3,
                Page = page,
            };
            try
            {
                var viewModel = new IndexViewModel<EventPreviewDto>
                {
                    Items = _mapper.Map<IEnumerable<EventPreviewDto>>(_eventService.VisitedEventsByUserId(id, model)),
                    PageViewModel = new PageViewModel(model.Count, model.Page, model.PageSize),
                };
                return Ok(viewModel);
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// This method gets  events. Used for notifications.
        /// </summary>
        /// <param name="eventIds">Reguired.</param>
        /// <param name="page">CountPages.</param>
        /// <returns>Events.</returns>
        /// <response code="200">Getting is successful.</response>
        /// <response code="400">Getting is failed.</response>
        [HttpPost("[action]")]
        public IActionResult GetEvents([FromBody] List<Guid> eventIds, [FromQuery] int page = 1)
        {
            var model = new PaginationViewModel
            {
                PageSize = 1,
                Page = page,
            };

            try
            {
                var viewModel = new IndexViewModel<EventPreviewDto>
                {
                    Items = _mapper.Map<IEnumerable<EventPreviewDto>>(
                        _eventService.GetEvents(eventIds, model)),
                    PageViewModel = new PageViewModel(model.Count, model.Page, model.PageSize),
                };
                return Ok(viewModel);
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest();
            }
        }
    }
}
