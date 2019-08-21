using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace EventsExpress.Core.ChatHub
{
    public class ChatRoom : Hub
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        public ChatRoom(
                IAuthService authService,
                IUserService userService,
                IMessageService messageService
            )
        {
            _authService = authService;
            _userService = userService;
            _messageService = messageService;
        }

        public async Task Send(Guid chatId, string text)
        {
            var user = _authService.GetCurrentUser(Context.User);
            var res = await _messageService.Send(chatId, user.Id, text);
                                                                                 
            var users = _messageService.GetChatUserIds(res.ChatRoomId);

            await Clients.Users(users).SendAsync("ReceiveMessage", res);        
        }

        public async Task Seen(List<Guid> msgIds)
        {

            var res = await _messageService.MsgSeen(msgIds);
                                                                   
            if (res.Successed)
            {
                var users = _messageService.GetChatUserIds(Guid.Parse(res.Property));
                await Clients.Users(users).SendAsync("WasSeen", msgIds);    
            }

        }
    }
}
