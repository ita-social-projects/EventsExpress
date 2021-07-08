using System;
using System.Collections.Generic;
using System.Linq;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Enums;
using EventsExpress.Hubs.Clients;
using Microsoft.AspNetCore.SignalR;

namespace EventsExpress.Hubs
{
    public class UsersHub : Hub<IUsersClient>
    {
        public static readonly string AdminsCacheKey = Guid.NewGuid().ToString();

        private readonly IUserService _userService;
        private readonly ICacheHelper _cacheHelper;

        public UsersHub(
            ICacheHelper cacheHelper,
            IUserService userService)
        {
            _cacheHelper = cacheHelper;
            _userService = userService;
        }

        private List<string> Admins
        {
            get
            {
                var cachedAdmins = _cacheHelper.GetValue(AdminsCacheKey);

                if (cachedAdmins != null)
                {
                    return (List<string>)cachedAdmins.Value;
                }

                var admins = _userService.GetUsersByRole(Role.Admin)
                    .Select(admin => admin.Id.ToString())
                    .ToList();

                _cacheHelper.Add(new CacheDto
                {
                    Key = AdminsCacheKey,
                    Value = admins,
                });

                return admins;
            }
        }
    }
}
