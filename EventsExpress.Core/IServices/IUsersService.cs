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

        UserDTO GetById(Guid id);

        UserDTO GetByEmail(string email);

        IEnumerable<UserDTO> GetAll();
        IEnumerable<UserDTO> GetCategoriesFollowers(IEnumerable<string> categories);
        IEnumerable<UserDTO> Get(Expression<Func<User, bool>> filter);
    }
}
