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

        Task ChangeRole(Guid uId, Guid rId);

        Task ChangeAvatar(Guid uId, IFormFile avatar);

        Task ConfirmEmail(CacheDto cacheDto);

        Task PasswordRecover(UserDto userDto);

        Task EditFavoriteCategories(UserDto user, IEnumerable<Category> categories);

        Task SetAttitude(AttitudeDto attitude);

        Task Block(Guid uId);

        Task Unblock(Guid uId);

        UserDto GetById(Guid id);

        UserDto GetByEmail(string email);

        UserDto GetUserByRefreshToken(string token);

        ProfileDto GetProfileById(Guid id, Guid fromId);

        IEnumerable<UserDto> Get(UsersFilterViewModel model, out int count, Guid id);

        IEnumerable<UserDto> GetUsersByCategories(IEnumerable<CategoryDto> categories);

        IEnumerable<UserDto> GetUsersByRole(string role);

        double GetRating(Guid userId);
    }
}
