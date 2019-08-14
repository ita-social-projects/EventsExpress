using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.EF;
using EventsExpress.DTO;
using EventsExpress.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {

        private IEventService _eventService;
        private IMapper _mapper;

        public EventController(
            IEventService eventService,
                    IMapper mapper, AppDbContext appDbContext)
        {
            _eventService = eventService;
            _mapper = mapper;
        }                    


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


        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult Get(Guid id)
        {
            var res = _mapper.Map<EventDTO, EventDto>(_eventService.EventById(id));

            return Ok(res);
        }
               
        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult All([FromQuery]EventFilterViewModel model)
        {
            model.PageSize = 6;

            int Count;

            var res = _mapper.Map<IEnumerable<EventDTO>, IEnumerable<EventPreviewDto>>(_eventService.Events(model, out Count));
                    
            PageViewModel pageViewModel = new PageViewModel(Count, model.Page, model.PageSize);
            if (pageViewModel.PageNumber > pageViewModel.TotalPages)
            {
                return BadRequest();
            }
            IndexViewModel<EventPreviewDto> viewModel = new IndexViewModel<EventPreviewDto>
            {
                PageViewModel = pageViewModel,
                items = res
            };
            return Ok(viewModel);
        
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public IActionResult AllForAdmin([FromQuery]EventFilterViewModel model)
        {
            model.PageSize = 6;
            int Count;
            var res = _mapper.Map<IEnumerable<EventDTO>, IEnumerable<EventPreviewDto>>(_eventService.Events(model, out Count));

            PageViewModel pageViewModel = new PageViewModel(Count, model.Page, model.PageSize);
            if (pageViewModel.PageNumber > pageViewModel.TotalPages)
            {
                return BadRequest();
            }
            IndexViewModel<EventPreviewDto> viewModel = new IndexViewModel<EventPreviewDto>
            {
                PageViewModel = pageViewModel,
                items = res
            };
            return Ok(viewModel);
        }


        [HttpPost("[action]")]
        public async Task<IActionResult> Edit([FromForm]EventDto model)
        {                                   
            var res = model.Id == Guid.Empty ? await _eventService.Create(_mapper.Map<EventDto, EventDTO>(model))
                                       : await _eventService.Edit(_mapper.Map<EventDto, EventDTO>(model));
            if (res.Successed)
            {
                return Ok();
            }                                
            return BadRequest(res.Message);
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult FutureEvents(Guid Id)
        {
            var res = _mapper.Map<IEnumerable<EventDTO>, IEnumerable<EventPreviewDto>>(_eventService.FutureEventsByUserId(Id));

            return Ok(res);
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult PastEvents(Guid Id)
        {
            var res = _mapper.Map<IEnumerable<EventDTO>, IEnumerable<EventPreviewDto>>(_eventService.PastEventsByUserId(Id));

            return Ok(res);
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult EventsToGo(Guid Id)
        {
            var res = _mapper.Map<IEnumerable<EventDTO>, IEnumerable<EventPreviewDto>>(_eventService.EventsToGoByUserId(Id));

            return Ok(res);
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult VisitedEvents(Guid Id)
        {
            var res = _mapper.Map<IEnumerable<EventDTO>, IEnumerable<EventPreviewDto>>(_eventService.VisitedEventsByUserId(Id));

            return Ok(res);
        }
    }
}