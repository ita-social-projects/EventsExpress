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
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services
{
    public class ContactAdminService : BaseService<ContactAdmin>, IContactAdminService
    {
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContactAdminService(
            AppDbContext context,
            IAuthService authService,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
            : base(context, mapper)
        {
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Guid> SendMessageToAdmin(ContactAdminDto contactAdminDto)
        {
            var contactAdminEntity = Mapper.Map<ContactAdminDto, ContactAdmin>(contactAdminDto);
            var result = Insert(contactAdminEntity);
            await Context.SaveChangesAsync();
            return result.Id;
        }

        public IEnumerable<ContactAdminDto> GetAll()
        {
            var contactAdminMessages = Context.ContactAdmin.Select(x => new ContactAdminDto
            {
                SenderId = x.Id,
                Title = x.Title,
                DateCreated = x.DateCreated,
                Status = x.Status,
            })
                .OrderBy(contactAdminMessages => contactAdminMessages.DateCreated);

            return contactAdminMessages;
        }
    }
}
