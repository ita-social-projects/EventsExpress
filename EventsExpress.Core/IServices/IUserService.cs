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
        Task Create(UserDTO userDto);

        Task Update(UserDTO userDto);

        Task ChangeRole(Guid uId, Guid rId);

        Task ChangeAvatar(Guid uId, IFormFile avatar);

        Task ConfirmEmail(CacheDTO cacheDto);

        Task PasswordRecover(UserDTO userDto);

        Task EditFavoriteCategories(UserDTO user, IEnumerable<Category> categories);

        Task SetAttitude(AttitudeDTO attitude);

        Task Block(Guid uId);

        Task Unblock(Guid uId);

        UserDTO GetById(Guid id);

        UserDTO GetByEmail(string email);

        UserDTO GetUserByRefreshToken(string token);

        ProfileDTO GetProfileById(Guid id, Guid fromId);

        IEnumerable<UserDTO> Get(UsersFilterViewModel model, out int count, Guid id);

        IEnumerable<UserDTO> GetUsersByCategories(IEnumerable<CategoryDTO> categories);

        IEnumerable<UserDTO> GetUsersByRole(string role);

        double GetRating(Guid userId);
    }
}
