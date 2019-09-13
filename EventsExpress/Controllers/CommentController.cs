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
        /// <summary>
        /// This method is for edit and create comments
        /// </summary>
        /// <param name="model">Required</param>
        /// <returns></returns>
        /// <response code="200">Edit/Create comment proces success</response>
        /// <response code="400">If Edit/Create process failed</response>
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

        /// <summary>
        /// This method is for delete comment
        /// </summary>
        /// <param name="id">Required</param>
        /// <returns></returns>
        /// <response code="200">Delete comment proces success</response>
        /// <response code="400">If delete process failed</response> 
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

        /// <summary>
        /// This method have to return all comments
        /// </summary>
        /// <param name="id">Required</param>
        /// <param name="page">Required</param>
        /// <returns></returns>
        /// <response code="200">Return CommentDto model</response>
        [AllowAnonymous]
        [HttpGet("[action]/{id}/")]
        public IActionResult All(Guid id, int page = 1)
        {
            int pageSize = 5;
            int count;
            var res = _mapper.Map<IEnumerable<CommentDto>>(
                _commentService
                    .GetCommentByEventId(id, page, pageSize, out count));

            foreach (var com in res)
            {
                com.Children = _mapper.Map<IEnumerable<CommentDto>>(com.Children);
            }

            var viewModel = new IndexViewModel<CommentDto>
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                Items = res
            };
            return Ok(viewModel);
        }
    }
    
}