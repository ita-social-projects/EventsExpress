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
        private readonly IPhotoService _photoService;

        public ChatController(
            IMessageService messageService,
            IAuthService authService,
            IMapper mapper,
            IPhotoService photoService)
        {
            _authService = authService;
            _messageService = messageService;
            _mapper = mapper;
            _photoService = photoService;
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
            var currentUser = _authService.GetCurrentUser(HttpContext.User);
            var res = _mapper.Map<IEnumerable<ChatRoom>, IEnumerable<UserChatViewModel>>(_messageService.GetUserChats(currentUser.Id), opt =>
            {
                opt.AfterMap((src, dest) =>
                {
                    foreach (var c in _messageService.GetUserChats(currentUser.Id))
                    {
                        foreach (var u in c.Users)
                        {

                        }
                    }
                });
            });
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
            var sender = _authService.GetCurrentUser(HttpContext.User);
            var chat = await _messageService.GetChat(chatId, sender.Id);
            if (chat == null)
            {
                return BadRequest();
            }

            return Ok(_mapper.Map<ChatRoom, ChatViewModel>(chat, opt =>
            {
                opt.AfterMap((src, dest) =>
                {
                    foreach (var d in dest.Users)
                    {
                        d.PhotoUrl = _photoService.GetPhotoFromAzureBlob($"users/{d.Id}/photo.png").Result;
                    }
                });
            }));
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
    }
}
