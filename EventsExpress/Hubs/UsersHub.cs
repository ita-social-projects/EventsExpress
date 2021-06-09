using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Enums;
using Microsoft.AspNetCore.SignalR;

namespace EventsExpress.Hubs
{
    using System.Collections.Generic;

    public class UsersHub : Hub
    {
        private readonly IUserService _userService;

        public UsersHub(IUserService userService)
        {
            _userService = userService;
        }

        private List<string> Admins => _userService.GetUsersByRole(Role.Admin)
            .Select(admin => admin.Id.ToString())
            .ToList();

        public async Task SendCountOfUsersAsync()
        {
            var numberOfUsers = await _userService.CountUsersAsync();

            await Clients.Users(Admins).SendAsync("CountUsers", numberOfUsers);
        }

        public async Task SendCountOfBlockedUsersAsync()
        {
            var numberOfUsers = await _userService.CountBlockedUsersAsync();

            await Clients.Users(Admins).SendAsync("CountBlockedUsers", numberOfUsers);
        }

        public async Task SendCountOfUnblockedUsersAsync()
        {
            var numberOfUsers = await _userService.CountUnblockedUsersAsync();

            await Clients.Users(Admins).SendAsync("CountUnblockedUsers", numberOfUsers);
        }
    }
}
