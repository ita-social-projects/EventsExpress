using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.IServices;
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
            var numberOfUsers = await _userService.CountAsync();
            var admins = _userService.GetUsersByRole("Admin")
                .Select(admin => admin.Id.ToString())
                .ToList();

            await Clients.Users(admins).SendAsync("CountUsers", numberOfUsers);
        }
    }
}
