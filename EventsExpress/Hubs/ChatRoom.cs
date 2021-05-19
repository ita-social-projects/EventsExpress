using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Bridge;
using Microsoft.AspNetCore.SignalR;

namespace EventsExpress.Hubs
{
    public class ChatRoom : Hub
    {
        private readonly IUserService _userService;
        private readonly IMessageService _messageService;
        private readonly IEventService _eventService;
        private readonly ISecurityContext _securityContextService;

        public ChatRoom(
                IUserService userService,
                IMessageService messageService,
                IEventService eventService,
                ISecurityContext securityContextService)
        {
            _userService = userService;
            _messageService = messageService;
            _eventService = eventService;
            _securityContextService = securityContextService;
        }

        public async Task Send(Guid chatId, string text)
        {
            text = text.Trim();
            if (text != string.Empty)
            {
                var currentUserId = _securityContextService.GetCurrentUserId();
                var res = await _messageService.Send(chatId, currentUserId, text);

                var users = _messageService.GetChatUserIds(res.ChatRoomId);

                await Clients.Users(users).SendAsync("ReceiveMessage", res);
            }
        }

        public async Task Seen(List<Guid> msgIds)
        {
            var res = await _messageService.MsgSeen(msgIds);
            var users = _messageService.GetChatUserIds(Guid.Parse(res.ToString()));
            await Clients.Users(users).SendAsync("WasSeen", msgIds);
        }

        public async Task EventWasCreated(Guid eventId)
        {
            var currentUserId = _securityContextService.GetCurrentUserId();
            var res = _eventService.EventById(eventId);
            var users = _userService.GetUsersByCategories(res.Categories).Where(x => x.Id != currentUserId).Select(x => x.Id.ToString()).ToList();

            await Clients.Users(users).SendAsync("ReceivedNewEvent", res.Id);
        }
    }
}
