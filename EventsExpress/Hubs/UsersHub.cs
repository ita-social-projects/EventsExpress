using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Enums;
using Microsoft.AspNetCore.SignalR;

namespace EventsExpress.Hubs
{
    public class UsersHub : Hub
    {
        private readonly IUserService _userService;

        public UsersHub(IUserService userService)
        {
            _userService = userService;
        }

        public async Task SendCountOfUsersAsync()
        {
            var users = await _userService.CountUsersAsync();
            var admins = _userService.GetUsersByRole(Role.Admin)
                .Select(admin => admin.Id.ToString())
                .ToList();

            await Clients.Users(admins).SendAsync("CountUsers", users);
        }
    }
}
