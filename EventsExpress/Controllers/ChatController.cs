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


        [HttpGet("[action]")]
        public IActionResult GetAllChats()
        {
            var currentUser = _authService.GetCurrentUser(HttpContext.User);
            var res = _mapper.Map<IEnumerable<UserChatDto>>(_messageService.GetUserChats(currentUser.Id));
            return Ok(res);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetChat([FromQuery]Guid chatId)
        {
            var sender = _authService.GetCurrentUser(HttpContext.User);
            var chat = await _messageService.GetChat(chatId, sender.Id);
            if(chat == null)
            {
                return BadRequest();
            }                                    
            return Ok(_mapper.Map<ChatDto>(chat));
        }

        [HttpGet("[action]")]
        public IActionResult GetUnreadMessages([FromQuery]Guid userId)
        {
            var res = _mapper.Map<IEnumerable<MessageDto>>(_messageService.GetUnreadMessages(userId));
            return Ok(res);
        }
    }
}