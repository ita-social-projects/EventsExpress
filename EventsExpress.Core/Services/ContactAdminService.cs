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
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services
{
    public class ContactAdminService : BaseService<ContactAdmin>, IContactAdminService
    {
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMediator _mediator;

        public ContactAdminService(
            AppDbContext context,
            IAuthService authService,
            IHttpContextAccessor httpContextAccessor,
            IMediator mediator,
            IMapper mapper)
            : base(context, mapper)
        {
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
            _mediator = mediator;
        }

        public async Task SetIssueStatus(Guid messageId, ContactAdminStatus issueStatus)
        {
            var message = Context.ContactAdmin.Find(messageId);
            if (message == null)
            {
                throw new EventsExpressException("Invalid message id");
            }

            var record = new ContactAdmin
            {
                MessageId = messageId,
                Status = issueStatus,
            };
            Insert(record);

            await Context.SaveChangesAsync();

         // await _mediator.Publish(new EventStatusMessage(eventId, reason, eventStatus));
        }

        public async Task<Guid> SendMessageToAdmin(ContactAdminDto contactAdminDto)
        {
            var contactAdminEntity = Mapper.Map<ContactAdminDto, ContactAdmin>(contactAdminDto);
            var result = Insert(contactAdminEntity);
            await Context.SaveChangesAsync();
            return result.Id;
        }

        public IEnumerable<ContactAdminDto> GetAll(Guid id, int page, int pageSize, out int count)
        {
            var contactAdminMessages = Context.ContactAdmin
                .Where(x => x.MessageId == id && x.SenderId == null)
                .AsNoTracking()
                .AsQueryable();
            count = contactAdminMessages.Count();
            var result = contactAdminMessages.OrderBy(x => x.DateCreated).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return Mapper.Map<IEnumerable<ContactAdmin>, IEnumerable<ContactAdminDto>>(result);
        }
    }
}
