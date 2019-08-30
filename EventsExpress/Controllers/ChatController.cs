using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.IServices;
using EventsExpress.DTO;
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
            IMapper mapper
            )
        {
            _authService = authService;
            _messageService = messageService;
            _mapper = mapper;
        }

        /// <summary>
        /// This method have to return all chats
        /// </summary>
        /// <returns>UserChatDto model</returns>
        /// <response code="200">UserChatDto model</response>
        /// <response code="200">If proccess is failed</response>
        [HttpGet("[action]")]
        public IActionResult GetAllChats()
        {
            var currentUser = _authService.GetCurrentUser(HttpContext.User);
            var res = _mapper.Map<IEnumerable<UserChatDto>>(_messageService.GetUserChats(currentUser.Id));
            return Ok(res);
        }

        /// <summary>
        /// This method have to return chat
        /// </summary>
        /// <param name="chatId">Required</param>
        /// <returns></returns>
        /// <response code="200">UserChatDto model</response>
        /// <response code="200">If proccess is failed</response>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetChat([FromQuery]Guid chatId)
        {
            var sender = _authService.GetCurrentUser(HttpContext.User);
            var chat = await _messageService.GetChat(chatId, sender.Id);
            var res = _mapper.Map<ChatDto>(chat);
            return Ok(res);
        }

        /// <summary>
        /// This method is to get mesagees which are unread
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// /// <returns></returns>
        /// <response code="200">MessageDto model</response>
        /// <response code="200">If proccess is failed</response>
        [HttpGet("[action]")]
        public IActionResult GetUnreadMessages([FromQuery]Guid userId)
        {
            var res = _mapper.Map<IEnumerable<MessageDto>>(_messageService.GetUnreadMessages(userId));
            return Ok(res);
        }
    }
}