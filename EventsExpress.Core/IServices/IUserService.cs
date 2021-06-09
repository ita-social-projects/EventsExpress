using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using Microsoft.AspNetCore.Http;
using Role = EventsExpress.Db.Enums.Role;

namespace EventsExpress.Core.IServices
{
    public interface IUserService
    {
        Task Create(UserDto userDto);

        Task<int> CountBlockedUsersAsync();

        Task<int> CountUnblockedUsersAsync();

        Task<int> CountUsersAsync();

        Task ChangeAvatar(Guid userId, IFormFile avatar);

        Task EditFavoriteCategories(IEnumerable<Category> categories);

        Task SetAttitude(AttitudeDto attitude);

        UserDto GetCurrentUserInfo();

        ProfileDto GetProfileById(Guid id);

        IEnumerable<UserDto> Get(UsersFilterViewModel model, out int count);

        IEnumerable<UserDto> GetUsersByCategories(IEnumerable<CategoryDto> categories);

        IEnumerable<UserDto> GetUsersByRole(Role role);

        double GetRating(Guid userId);

        IEnumerable<UserDto> GetUsersByNotificationTypes(NotificationChange notificationType, IEnumerable<Guid> userIds);

        Task<Guid> EditFavoriteNotificationTypes(IEnumerable<NotificationType> notificationTypes);

        Task EditUserName(string name);

        Task EditBirthday(DateTime birthday);

        Task EditGender(Gender gender);
    }
}
