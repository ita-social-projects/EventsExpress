using System;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Db.BaseService;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using Microsoft.AspNetCore.Http;

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
            var message = Context.ContactAdmin
                .Find(_authService.GetCurrentUser(_httpContextAccessor.HttpContext.User).Id);
            if (message == null)
            {
                throw new EventsExpressException("Not found");
            }

            var contactAdminEntity = Mapper.Map<ContactAdminDto, ContactAdmin>(contactAdminDto);
            var result = Insert(contactAdminEntity);

            // var currentUser = _authService.GetCurrentUser(_httpContextAccessor.HttpContext.User);
            // var message = Context.ContactAdmin.Find(currentUser.Id);
            // message.SenderId = currentUser.Id;
            // message.Subject = contactAdminDto.Subject;
            // message.EmailBody = contactAdminDto.MessageText;
            // message.Email = contactAdminDto.Email;
            // message.Title = contactAdminDto.Title;
            await Context.SaveChangesAsync();
            return result.Id;
        }
    }
}
