using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Db.Entities;
using Microsoft.AspNetCore.Http;

namespace EventsExpress.Core.IServices
{
    public interface IUserService
    {
        Task<OperationResult> Create(UserDTO userDto);

        Task<OperationResult> Update(UserDTO userDto);

        Task<OperationResult> ChangeRole(Guid uId, Guid rId);

        Task<OperationResult> ChangeAvatar(Guid uId, IFormFile avatar);

        Task<OperationResult> ConfirmEmail(CacheDTO cacheDto);

        Task<OperationResult> PasswordRecover(UserDTO userDto);

        Task<OperationResult> EditFavoriteCategories(UserDTO user, IEnumerable<Category> categories);

        Task<OperationResult> SetAttitude(AttitudeDTO attitude);

        Task<OperationResult> Block(Guid uId);

        Task<OperationResult> Unblock(Guid uId);

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
