using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(
            ICommentService commentService,
            IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        /// <summary>
        /// This method is for edit and create comments.
        /// </summary>
        /// <param name="model">Param defines CommentViewModel model.</param>
        /// <returns>The method returns edited comment.</returns>
        /// <response code="200">Edit/Create comment proces success.</response>
        /// <response code="400">If Edit/Create process failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> Edit(CommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            await _commentService.Create(_mapper.Map<CommentDto>(model));

            return Ok();
        }

        /// <summary>
        /// This method is for delete comment.
        /// </summary>
        /// <param name="id">Param id defines comment identifier.</param>
        /// <returns>The method returns deleted comment.</returns>
        /// <response code="200">Delete comment proces success.</response>
        /// <response code="400">If delete process failed.</response>
        [HttpPost("{id}/[action]")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _commentService.Delete(id);

            return Ok();
        }

        /// <summary>
        /// This method have to return all comments.
        /// </summary>
        /// <param name="id">Param id defines comment identifier.</param>
        /// <param name="page">Param page defines page count.</param>
        /// <returns>The method returns all comments.</returns>
        /// <response code="200">Return CommentDto model.</response>
        [AllowAnonymous]
        [HttpGet("[action]/{id}/")]
        public IActionResult All(Guid id, int page = 1)
        {
            int pageSize = 5;
            var res = _mapper.Map<IEnumerable<CommentViewModel>>(_commentService.GetCommentByEventId(id, page, pageSize, out int count));

            foreach (var com in res)
            {
                com.Children = _mapper.Map<IEnumerable<CommentViewModel>>(com.Children);
            }

            var viewModel = new IndexViewModel<CommentViewModel>
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                Items = res,
            };

            return Ok(viewModel);
        }
    }
}
