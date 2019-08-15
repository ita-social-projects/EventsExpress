using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Db.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EventsExpress.Core.IServices
{
    public interface IUserService
    {
        Task<OperationResult> Create(UserDTO userDto);
        Task<OperationResult> Update(UserDTO userDTO);

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

        ProfileDTO GetProfileById(Guid id, Guid FromId);

        IEnumerable<UserDTO> Get(UsersFilterViewModel model, out int count);
        IEnumerable<UserDTO> GetUsersByCategories(IEnumerable<CategoryDTO> categories);
        
        AttitudeDTO GetAttitude(AttitudeDTO attitude);
    }
}
