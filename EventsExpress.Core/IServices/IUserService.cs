using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using Microsoft.AspNetCore.Http;

namespace EventsExpress.Core.IServices
{
    public interface IUserService
    {
        Task Create(UserDto userDto);

        Task Update(UserDto userDto);

        Task ChangeRole(Guid userId, Guid roleId);

        Task ChangeAvatar(Guid userId, IFormFile avatar);

        Task ConfirmEmail(CacheDto cacheDto);

        Task PasswordRecover(UserDto userDto);

        Task EditFavoriteCategories(UserDto userDto, IEnumerable<Category> categories);

        Task SetAttitude(AttitudeDto attitude);

        Task Block(Guid userId);

        Task Unblock(Guid userId);

        UserDto GetById(Guid userId);

        UserDto GetByEmail(string email);

        UserDto GetUserByRefreshToken(string token);

        ProfileDto GetProfileById(Guid userId, Guid fromId);

        IEnumerable<UserDto> Get(UsersFilterViewModel model, out int count, Guid id);

        IEnumerable<UserDto> GetUsersByCategories(IEnumerable<CategoryDto> categories);

        IEnumerable<UserDto> GetUsersByRole(string role);

        double GetRating(Guid userId);
    }
}
