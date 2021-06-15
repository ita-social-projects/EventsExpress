using System.Collections.Generic;
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
            Admins = _userService.GetUsersByRole(Role.Admin)
                .Select(admin => admin.Id.ToString())
                .ToList();
        }

        private List<string> Admins { get; }

        public async Task SendCountOfUsersAsync()
        {
            var numberOfUsers = await _userService.CountUsersAsync(AccountStatus.All);

            await Clients.Users(Admins).SendAsync("CountUsers", numberOfUsers);
        }

        public async Task SendCountOfBlockedUsersAsync()
        {
            var numberOfUsers = await _userService.CountUsersAsync(AccountStatus.Activated);

            await Clients.Users(Admins).SendAsync("CountBlockedUsers", numberOfUsers);
        }

        public async Task SendCountOfUnblockedUsersAsync()
        {
            var numberOfUsers = await _userService.CountUsersAsync(AccountStatus.Blocked);

            await Clients.Users(Admins).SendAsync("CountUnblockedUsers", numberOfUsers);
        }
    }
}
