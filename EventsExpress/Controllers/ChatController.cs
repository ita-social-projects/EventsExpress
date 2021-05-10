using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ChatController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMessageService _messageService;
        private readonly IAuthService _authService;

        public ChatController(
            IMessageService messageService,
            IAuthService authService,
            IMapper mapper)
        {
            _authService = authService;
            _messageService = messageService;
            _mapper = mapper;
        }

        /// <summary>
        /// This method have to return all chats.
        /// </summary>
        /// <returns>The method returns a list of user's chats.</returns>
        /// <response code="200">UserChatDto model.</response>
        /// <response code="200">If proccess is failed.</response>
        [HttpGet("[action]")]
        public IActionResult All()
        {
            var currentUserId = CurrentUserId();
            var res = _mapper.Map<IEnumerable<UserChatViewModel>>(_messageService.GetUserChats(currentUserId));
            return Ok(res);
        }

        /// <summary>
        /// This method have to return chat.
        /// </summary>
        /// <param name="chatId">Param chatId defines the chat identifier.</param>
        /// <returns>The method returns a Chat by identifier.</returns>
        /// <response code="200">UserChatDto model.</response>
        /// <response code="200">If proccess is failed.</response>
        [HttpGet("{chatId}")]
        public async Task<IActionResult> GetChat(Guid chatId)
        {
            var senderId = CurrentUserId();
            var chat = await _messageService.GetChat(chatId, senderId);
            if (chat == null)
            {
                return BadRequest();
            }

            return Ok(_mapper.Map<ChatViewModel>(chat));
        }

        /// <summary>
        /// This method is to get mesagees which are unread.
        /// </summary>
        /// <param name="userId">Param userId defines the user identifier.</param>
        /// <returns>The method returns unread messages for user.</returns>
        /// <response code="200">MessageDto model.</response>
        /// <response code="200">If proccess is failed.</response>
        [HttpGet("[action]")]
        public IActionResult GetUnreadMessages([FromQuery] Guid userId)
        {
            var res = _mapper.Map<IEnumerable<MessageViewModel>>(_messageService.GetUnreadMessages(userId));
            return Ok(res);
        }

        [NonAction]
        private Guid CurrentUserId() =>
            _authService.GetCurrentUserId(HttpContext.User);
    }
}
