using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class CommentController : ControllerBase
    {
        private ICommentService _commentService;
        private IMapper _mapper;
      
        public CommentController(ICommentService commentService,
                    IMapper mapper)
        { 
            _commentService = commentService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Edit(CommentDto model)
        {
            if (!ModelState.IsValid) {
                return BadRequest();
            }
            var res = await _commentService.Create(_mapper.Map<CommentDto, CommentDTO>(model));
                                       
            if (res.Successed)
            {
                return Ok();
            }
            return BadRequest(res.Message);
        }

        [AllowAnonymous]
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var res = await _commentService.Delete(id);

            if (res.Successed)
            {
                return Ok();
            }
            return BadRequest(res.Message);
        }

        [AllowAnonymous]
        [HttpGet("[action]/{id}/")]
        public IActionResult All(Guid id, int page = 1)
        {
            int pageSize = 5;
            int count;
            var res = _mapper.Map<IEnumerable<CommentDTO>, IEnumerable<CommentDto>>(_commentService.GetCommentByEventId(id, page, pageSize, out count));

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel<CommentDto> viewModel = new IndexViewModel<CommentDto>
            {
                PageViewModel = pageViewModel,
                Items = res
            };
            return Ok(viewModel);
        }
    }
    
}