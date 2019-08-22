using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core;
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
        private readonly IMapper _mapper;

        public EventController(
            IEventService eventService,
            IMapper mapper)
        {
            _eventService = eventService;
            _mapper = mapper;
        }

        /// <summary>
        /// This method is for edit and create events
        /// </summary>
        /// <param name="model">Required</param>
        /// <returns></returns>
        /// <response code="200">Edit/Create event proces success</response>
        /// <response code="400">If Edit/Create process failed</response> 
        [HttpPost("[action]")]
        public async Task<IActionResult> Edit([FromForm]EventDto model)
        {
            var result = model.Id == Guid.Empty 
                ? await _eventService.Create(_mapper.Map<EventDTO>(model))
                : await _eventService.Edit(_mapper.Map<EventDTO>(model));
            if (result.Successed)
            {
                return Ok();
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// This method have to return event
        /// </summary>
        /// <param name="id">Required</param>
        /// <returns></returns>
        /// <response code="200">Return UserInfo model</response>
        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult Get(Guid id) => 
            Ok(_mapper.Map<EventDto>(_eventService.EventById(id)));

        /// <summary>
        /// This method have to return all events
        /// </summary>
        /// <param name="filter">Required</param>
        /// <returns></returns>
        /// <response code="200">Return IEnumerable EventPreviewDto</response>
        /// <response code="400">If return failed</response>       
        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult All([FromQuery]EventFilterViewModel filter)
        {
            filter.PageSize = 6;
            try
            {
                var viewModel = new IndexViewModel<EventPreviewDto>
                {
                    Items = _mapper.Map<IEnumerable<EventPreviewDto>>(_eventService.Events(filter, out int count)),
                    PageViewModel = new PageViewModel(count, filter.Page, filter.PageSize)
                    
                };
                return Ok(viewModel);
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// This method have to return all events fro Admin
        /// </summary>
        /// <param name="filter">Required</param>
        /// <returns></returns>
        /// <response code="200">Return IEnumerable EventPreviewDto</response>
        /// <response code="400">If return failed</response>  
        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public IActionResult AllForAdmin([FromQuery]EventFilterViewModel filter)
        {
            filter.PageSize = 6;
            try
            {
                var viewModel = new IndexViewModel<EventPreviewDto>
                {
                    Items = _mapper.Map<IEnumerable<EventPreviewDto>>(_eventService.EventsForAdmin(filter, out int count)),
                    PageViewModel = new PageViewModel(count, filter.Page, filter.PageSize)
                    
                };
                return Ok(viewModel);
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// This method have to add user to category
        /// </summary>
        /// <param name="userId">Required</param>
        /// <param name="eventId">Required</param>
        /// <returns></returns>
        /// <response code="200">Adding user from event proces success</response>
        /// <response code="400">If adding user from event process failed</response>        
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
        /// This method have to add user to category
        /// </summary>
        /// <param name="userId">Required</param>
        /// <param name="eventId">Required</param>
        /// <returns></returns>
        /// <response code="200">Delete  user from event proces success</response>
        /// <response code="400">If deleting user from event process failed</response>
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
        /// This method is to block event
        /// </summary>
        /// <param name="eventId">Required</param>
        /// <returns></returns>
        /// <response code="200">Block is succesful</response>
        /// <response code="400">Block process failed</response>
        [HttpPost("[action]")]
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
        /// This method is to unblock event
        /// </summary>
        /// <param name="eventId">Required</param>
        /// <returns></returns>
        /// <response code="200">Unblock is succesful</response>
        /// <response code="400">Unblock process failed</response>
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

        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult FutureEvents(Guid id) =>
            Ok(_mapper.Map<IEnumerable<EventPreviewDto>>(_eventService.FutureEventsByUserId(id)));


        [HttpGet("[action]")]
        public IActionResult PastEvents(Guid id) => 
            Ok(_mapper.Map<IEnumerable<EventPreviewDto>>(_eventService.PastEventsByUserId(id)));


        [HttpGet("[action]")]
        public IActionResult EventsToGo(Guid id) => 
            Ok(_mapper.Map<IEnumerable<EventPreviewDto>>(_eventService.EventsToGoByUserId(id)));


        [HttpGet("[action]")]
        public IActionResult VisitedEvents(Guid id) => 
            Ok(_mapper.Map<IEnumerable<EventPreviewDto>>(_eventService.VisitedEventsByUserId(id)));
        


    }
}