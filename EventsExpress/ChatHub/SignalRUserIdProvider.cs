using Microsoft.AspNetCore.SignalR;

namespace EventsExpress.Core.ChatHub
{
    public class SignalRUserIdProvider : IUserIdProvider
    {
        public string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.Identity?.Name;
        }
    }
}
