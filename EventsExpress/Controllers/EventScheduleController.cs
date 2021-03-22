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
        private readonly IPhotoService _photoService;

        public EventScheduleController(
            IEventScheduleService eventScheduleService,
            IMapper mapper,
            IPhotoService photoService)
        {
            _eventScheduleService = eventScheduleService;
            _mapper = mapper;
            _photoService = photoService;
        }

        /// <summary>
        /// This method have to return all event schedules.
        /// </summary>
        /// <returns>The method returns event schedules.</returns>
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
                    Items = _mapper.Map<IEnumerable<EventScheduleDto>, IEnumerable<PreviewEventScheduleViewModel>>(
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
        /// This method is for edit event schedule.
        /// </summary>
        /// <param name="eventId">Param eventId defines the event identifier.</param>
        /// <param name="model">Param model provides access to event schedule properties.</param>
        /// <returns>The method returns edited event.</returns>
        /// <response code="200">Edit/Create event proces success.</response>
        /// <response code="400">If Edit/Create process failed.</response>
        [HttpPost("{eventId:Guid}/[action]")]
        [UserAccessTypeFilterAttribute]
        public async Task<IActionResult> Edit(Guid eventId, [FromForm] PreviewEventScheduleViewModel model)
        {
            var result = await _eventScheduleService.Edit(_mapper.Map<PreviewEventScheduleViewModel, EventScheduleDto>(model));

            return Ok(result);
        }

        /// <summary>
        /// This method is for edit and create events.
        /// </summary>
        /// <param name="eventId">Param eventId defines the event identifier.</param>
        /// <returns>The method returns canceled all event schedules.</returns>
        /// <response code="200">Cancel All Events proces success.</response>
        /// <response code="400">Cancel All Events process failed.</response>
        [HttpPost("{eventId:Guid}/[action]")]
        [UserAccessTypeFilterAttribute]
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
        /// <param name="eventId">Param eventId defines the event identifier.</param>
        /// <returns>The method returns canceled next event schedule.</returns>
        /// <response code="200">Cancel Next Event event proces success.</response>
        /// <response code="400">Cancel Next Event process failed.</response>
        [HttpPost("{eventId:Guid}/[action]")]
        [UserAccessTypeFilterAttribute]
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
        /// <param name="eventScheduleId">Param eventScheduleId defines the event schedule identifier.</param>
        /// <returns>The method returns event schedule by identifier.</returns>
        /// <response code="200">Return UserInfo model.</response>
        [AllowAnonymous]
        [HttpGet("{eventScheduleId:Guid}")]
        public IActionResult Get(Guid eventScheduleId) =>
            Ok(_mapper.Map<EventScheduleDto, EventScheduleViewModel>(_eventScheduleService.EventScheduleById(eventScheduleId)));
    }
}
