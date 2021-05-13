using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Db.BaseService;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services
{
    public class ContactAdminService : BaseService<ContactAdmin>, IContactAdminService
    {
        public ContactAdminService(
            AppDbContext context,
            IMapper mapper)
            : base(context, mapper)
        {
        }

        public async Task SetIssueStatus(Guid messageId, ContactAdminStatus issueStatus)
        {
            var message = Context.ContactAdmin.Find(messageId);
            if (message == null)
            {
                throw new EventsExpressException("Invalid message id");
            }

            message.Status = issueStatus;
            Update(message);

            await Context.SaveChangesAsync();
        }

        public ContactAdminDto MessageById(Guid messageId)
        {
            var res = Mapper.Map<ContactAdminDto>(
                Context.ContactAdmin
                .FirstOrDefault(x => x.Id == messageId));

            return res;
        }

        public async Task<Guid> SendMessageToAdmin(ContactAdminDto contactAdminDto)
        {
            var contactAdminEntity = Mapper.Map<ContactAdminDto, ContactAdmin>(contactAdminDto);
            var result = Insert(contactAdminEntity);
            await Context.SaveChangesAsync();
            return result.Id;
        }

        public IEnumerable<ContactAdminDto> GetAll(ContactAdminFilterViewModel model, Guid id, out int count)
        {
            var contactAdminMessages = Context.ContactAdmin
                .Where(x => x.MessageId == id)
                .AsNoTracking()
                .AsQueryable();

            contactAdminMessages = (model.DateCreated != null)
                ? contactAdminMessages.Where(x => x.Status == model.Status)
                .OrderBy(h => h.DateCreated)
                : contactAdminMessages;

            contactAdminMessages = (model.DateCreated != DateTime.MinValue)
                ? contactAdminMessages.Where(x => x.DateCreated == model.DateCreated)
                : contactAdminMessages;

            count = contactAdminMessages.Count();
            var result = contactAdminMessages
                .OrderBy(x => x.DateCreated)
                .Skip((model.Page - 1) * model.PageSize)
                .Take(model.PageSize).ToList();
            return Mapper.Map<IEnumerable<ContactAdmin>, IEnumerable<ContactAdminDto>>(result);
        }
    }
}
