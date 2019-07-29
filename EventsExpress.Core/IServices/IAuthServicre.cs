using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Db.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EventsExpress.Core.IServices
{
    public interface IAuthServicre
    {
        OperationResult Authenticate(string email, string password);
        
        UserDTO GetCurrentUser(ClaimsPrincipal userClaims);
        bool CheckPassword(string currentPassword, string oldPassword);
    }
}
