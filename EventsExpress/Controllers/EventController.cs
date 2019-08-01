using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.DTO;
using EventsExpress.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {

        private IEventService _eventService;
        private IMapper _mapper;
        private AppDbContext _appDbContext;
        public EventController(IEventService eventService,
                    IMapper mapper, AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _eventService = eventService;
            _mapper = mapper;
        }                    

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteUserFromEvent(Guid userId, Guid eventId)
        {
            var res = await _eventService.DeleteUserFromEvent(userId, eventId);
            if (res.Successed)
                return Ok();
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> AddUserToEvent(Guid userId, Guid eventId)
        {
            var res = await _eventService.AddUserToEvent(userId, eventId);
            if (res.Successed)
                return Ok();
            return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult Get(Guid id)
        {

            var res = _mapper.Map<EventDTO, EventDto>(_eventService.EventById(id));

            return Ok(res);
        }

        //[AllowAnonymous]
        //[HttpGet("[action]")]
        //public IActionResult All()
        //{
        //    var res = _mapper.Map<IEnumerable<EventDTO>, IEnumerable<EventDto>>(_eventService.Events());

        //    return Ok(res);
        //}
        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult All(int page = 1)
        {
            int pageSize = 1;
          
            var res = _mapper.Map<IEnumerable<EventDTO>, IEnumerable<EventDto>>(_eventService.Events(page));

            var count = _appDbContext.Events.Count();
            


            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Events = res
            };
            return Ok(viewModel);
        
        }

        [AllowAnonymous]
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

    }
}