using EventsExpress.Hubs.Clients;
using Microsoft.AspNetCore.SignalR;

namespace EventsExpress.Hubs
{
    public class UsersHub : Hub<IUsersClient>
    {
        public UsersHub()
        {
        }
    }
}
