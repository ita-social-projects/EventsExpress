using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;

namespace EventsExpress.Core.IServices
{
    public interface IContactAdminService
    {
        Task<Guid> SendMessageToAdmin(ContactAdminDto contactAdminDto);

        IEnumerable<ContactAdminDto> GetAll();
    }
}
