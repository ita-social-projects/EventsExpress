using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
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

        public async Task UpdateIssueStatus(Guid messageId, string resolutionDetails, ContactAdminStatus issueStatus)
        {
            var message = Context.ContactAdmin.Find(messageId);
            if (message == null)
            {
                throw new EventsExpressException("Invalid message Id");
            }

            message.Status = issueStatus;
            message.ResolutionDetails = resolutionDetails;
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
            if (contactAdminDto.Title == null)
            {
                contactAdminDto.Title = $"New request from {contactAdminDto.Email} on subject: {contactAdminDto.Subject}";
            }

            var contactAdminEntity = Mapper.Map<ContactAdminDto, ContactAdmin>(contactAdminDto);
            var result = Insert(contactAdminEntity);
            await Context.SaveChangesAsync();
            return result.Id;
        }

        public IEnumerable<ContactAdminDto> GetAll(ContactAdminFilterViewModel model, out int count)
        {
            IQueryable<ContactAdmin> contactAdminMessages = Context.ContactAdmin
                .AsNoTracking();

            contactAdminMessages = (model.Status != null)
                ? contactAdminMessages.Where(x => model.Status.Contains(x.Status))
                : contactAdminMessages;

            contactAdminMessages = (model.DateFrom != DateTime.MinValue)
                ? contactAdminMessages.Where(x => x.DateCreated >= model.DateFrom)
                : contactAdminMessages;

            contactAdminMessages = (model.DateTo != DateTime.MinValue)
                ? contactAdminMessages.Where(x => x.DateCreated <= model.DateTo)
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
