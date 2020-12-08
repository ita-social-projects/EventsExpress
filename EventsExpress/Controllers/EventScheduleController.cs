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
        private readonly IMapper _mapper;

        public EventScheduleController(
            IEventScheduleService eventScheduleService,
            IMapper mapper)
        {
            _eventScheduleService = eventScheduleService;
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
                var viewModel = new IndexViewModel<PreviewEventScheduleViewModel>
                {
                    Items = _mapper.Map<IEnumerable<PreviewEventScheduleViewModel>>(
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
        /// <param name="eventId">Required.</param>
        /// <param name="model">Required.</param>
        /// <response code="200">Edit/Create event proces success.</response>
        /// <response code="400">If Edit/Create process failed.</response>
        [HttpPost("{eventId:Guid}/[action]")]
        [UserAccessTypeFilter]
        public async Task<IActionResult> Edit(Guid eventId, [FromForm] PreviewEventScheduleViewModel model)
        {
            var result = await _eventScheduleService.Edit(_mapper.Map<EventScheduleDTO>(model));

            return Ok(result);
        }

        /// <summary>
        /// This method is for edit and create events.
        /// </summary>
        /// <param name="eventId">Required.</param>
        /// <response code="200">Cancel All Events proces success.</response>
        /// <response code="400">Cancel All Events process failed.</response>
        [HttpPost("{eventId:Guid}/[action]")]
        [UserAccessTypeFilter]
        public async Task<IActionResult> CancelAllEvents(Guid eventId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _eventScheduleService.CancelEvents(eventId);

            return Ok(result);
        }

        /// <summary>
        /// This method is for edit and create events.
        /// </summary>
        /// <param name="eventId">Required.</param>
        /// <response code="200">Cancel Next Event event proces success.</response>
        /// <response code="400">Cancel Next Event process failed.</response>
        [HttpPost("{eventId:Guid}/[action]")]
        [UserAccessTypeFilter]
        public async Task<IActionResult> CancelNextEvent(Guid eventId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _eventScheduleService.CancelNextEvent(eventId);

            return Ok(new { id = result });
        }

        /// <summary>
        /// This method have to return event.
        /// </summary>
        /// <param name="eventScheduleId">Required.</param>
        /// <returns>Event.</returns>
        /// <response code="200">Return UserInfo model.</response>
        [AllowAnonymous]
        [HttpGet("{eventScheduleId:Guid}")]
        public IActionResult Get(Guid eventScheduleId) =>
            Ok(_mapper.Map<EventScheduleViewModel>(_eventScheduleService.EventScheduleById(eventScheduleId)));
    }
}
