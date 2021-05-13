using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Enums;

namespace EventsExpress.Core.IServices
{
    public interface IContactAdminService
    {
        Task<Guid> SendMessageToAdmin(ContactAdminDto contactAdminDto);

        IEnumerable<ContactAdminDto> GetAll(ContactAdminFilterViewModel model, Guid id, out int count);

        Task SetIssueStatus(Guid messageId, ContactAdminStatus issueStatus);

        ContactAdminDto MessageById(Guid messageId);
    }
}
