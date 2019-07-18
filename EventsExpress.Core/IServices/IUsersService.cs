using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Db.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventsExpress.Core.IServices
{
    public interface IUsersService
    {
        Task<OperationResult> Create(UserDTO userDto);

        //  Task<User> GetCurrentUserAsync(HttpContext context);

        Task<OperationResult> Update(UserDTO userDTO);

        UserDTO GetById(Guid id);

        UserDTO GetByEmail(string email);

        IEnumerable<UserDTO> GetAll();
    }
}
