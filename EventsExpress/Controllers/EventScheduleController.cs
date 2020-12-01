using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Filters;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class EventScheduleController : ControllerBase
    {
        private readonly IEventScheduleService _eventScheduleService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public EventScheduleController(
            IEventScheduleService eventScheduleService,
            IAuthService authSrv,
            IMapper mapper)
        {
            _eventScheduleService = eventScheduleService;
            _authService = authSrv;
            _mapper = mapper;
        }

        /// <summary>
        /// This method have to return all events.
        /// </summary>
        /// <returns>AllEvents.</returns>
        /// <response code="200">Return IEnumerable EventPreviewDto.</response>
        /// <response code="400">If return failed.</response>
        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult All()
        {
            try
            {
                var viewModel = new IndexViewModel<EventScheduleViewModel>
                {
                    Items = _mapper.Map<IEnumerable<EventScheduleViewModel>>(
                        _eventScheduleService.GetAll()),
                };
                return Ok(viewModel);
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// This method is for edit and create events.
        /// </summary>
        /// <param name="model">Required.</param>
        /// <response code="200">Edit/Create event proces success.</response>
        /// <response code="400">If Edit/Create process failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> Edit([FromForm] EventScheduleViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = model.Id == Guid.Empty
                ? null
                : await _eventScheduleService.Edit(_mapper.Map<EventScheduleDTO>(model));

            if (result.Successed)
            {
                return Ok(result.Property);
            }

            return BadRequest(result.Message);
        }

        /// <summary>
        /// This method is for edit and create events.
        /// </summary>
        /// <param name="eventId">Required.</param>
        /// <response code="200">Cancel All Events proces success.</response>
        /// <response code="400">Cancel All Events process failed.</response>
        [HttpPost("[action]")]
        [UserAccessTypeFilter]
        public async Task<IActionResult> CancelAllEvents(Guid eventId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = eventId == Guid.Empty
                ? null
                : await _eventScheduleService.CancelEvents(eventId);

            if (result.Successed)
            {
                return Ok(result.Property);
            }

            return BadRequest(result.Message);
        }

        /// <summary>
        /// This method is for edit and create events.
        /// </summary>
        /// <param name="eventId">Required.</param>
        /// <response code="200">Cancel Next Event event proces success.</response>
        /// <response code="400">Cancel Next Event process failed.</response>
        [HttpPost("[action]")]
        [UserAccessTypeFilter]
        public async Task<IActionResult> CancelNextEvent(Guid eventId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = eventId == Guid.Empty
                ? null
                : await _eventScheduleService.CancelNextEvent(eventId);

            if (result.Successed)
            {
                return Ok(new { id = result.Property });
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
            Ok(_mapper.Map<EventScheduleViewModel>(_eventScheduleService.EventScheduleById(id)));
    }
}
