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
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OccurenceEventController : ControllerBase
    {
        private readonly IOccurenceEventService _occurenceEventService;
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;

        public OccurenceEventController(
            IOccurenceEventService occurenceEventService,
            IAuthService authSrv,
            IMapper mapper)
        {
            _occurenceEventService = occurenceEventService;
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
                var viewModel = new IndexViewModel<OccurenceEventDto>
                {
                    Items = _mapper.Map<IEnumerable<OccurenceEventDto>>(
                        _occurenceEventService.GetAll()),
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
        public async Task<IActionResult> Edit([FromForm] OccurenceEventDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = model.Id == Guid.Empty
                ? null
                : await _occurenceEventService.Edit(_mapper.Map<OccurenceEventDTO>(model));

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
        public async Task<IActionResult> CancelAllEvents(Guid eventId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = eventId == Guid.Empty
                ? null
                : await _occurenceEventService.CancelEvents(eventId);

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
        public async Task<IActionResult> CancelNextEvent(Guid eventId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = eventId == Guid.Empty
                ? null
                : await _occurenceEventService.CancelNextEvent(eventId);

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
            Ok(_mapper.Map<OccurenceEventDto>(_occurenceEventService.EventById(id)));
    }
}
