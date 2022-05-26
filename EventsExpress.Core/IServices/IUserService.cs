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

        Task<int> CountUsersAsync(AccountStatus status);

        Task<Guid> ChangeAvatar(IFormFile avatar);

        Task EditFavoriteCategories(IEnumerable<Category> categories);

        Task SetAttitude(AttitudeDto attitude);

        UserDto GetCurrentUserInfo();

        IEnumerable<NotificationTypeDto> GetUserNotificationTypes();

        IEnumerable<CategoryDto> GetUserCategories();

        ProfileDto GetProfileById(Guid id);

        IEnumerable<UserDto> Get(UsersFilterViewModel model, out int count);

        IEnumerable<UserDto> GetUsersInformationByIds(IEnumerable<Guid> ids);

        IEnumerable<UserDto> GetUsersByCategories(IEnumerable<CategoryDto> categories);

        IEnumerable<UserDto> GetUsersByRole(Role role);

        double GetRating(Guid userId);

        IEnumerable<UserDto> GetUsersByNotificationTypes(NotificationChange notificationType, IEnumerable<Guid> userIds);

        Task<Guid> EditFavoriteNotificationTypes(IEnumerable<NotificationType> notificationTypes);

        Task EditBirthday(DateTime birthday);

        Task EditGender(Gender gender);

        Task EditLocation(LocationDto location);

        Task EditFirstName(string firstName);

        Task EditLastName(string lastName);
    }
}
