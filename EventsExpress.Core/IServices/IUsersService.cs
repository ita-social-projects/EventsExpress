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

        //  Task<User> GetCurrentUserAsync(HttpContext context);

        Task<OperationResult> Update(UserDTO userDTO);
        Task<OperationResult> ChangeRole(Guid uId, Guid rId);
        Task<OperationResult> ChangeAvatar(Guid uId, IFormFile avatar);
        Task<OperationResult> Unblock(Guid uId);
         Task<OperationResult> Verificate(CacheDTO cacheDto);
        Task<OperationResult> PasswordRecover(UserDTO userDto);
        Task<OperationResult> Block(Guid uId);

        UserDTO GetById(Guid id);
        ProfileDTO GetProfileById(Guid id, Guid FromId);

        UserDTO GetByEmail(string email);

        Task<OperationResult> EditFavoriteCategories(UserDTO user, IEnumerable<Category> categories);

        IEnumerable<UserDTO> GetAll(UsersFilterViewModel model, out int Count);
        IEnumerable<UserDTO> GetCategoriesFollowers(IEnumerable<CategoryDTO> categories);
        IEnumerable<UserDTO> Get(Expression<Func<User, bool>> filter);

        Task<OperationResult> SetAttitude(AttitudeDTO attitude);
        AttitudeDTO GetAttitude(AttitudeDTO attitude);
    }
}
