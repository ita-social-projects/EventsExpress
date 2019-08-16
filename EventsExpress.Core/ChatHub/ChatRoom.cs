using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace EventsExpress.Core.ChatHub
{
    public class ChatRoom : Hub
    {
        private readonly IAuthServicre _authService;
        public ChatRoom(
                IAuthServicre authService
            )
        {
            _authService = authService;
        }

        public async Task Send(int num)
        {
            var user = _authService.GetCurrentUser(Context.User);
            await Clients.All.SendAsync("ReceiveMessage", num);
        }
    }
}
