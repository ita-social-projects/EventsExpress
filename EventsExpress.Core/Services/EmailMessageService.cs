using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.BaseService;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services
{
    public class EmailMessageService : BaseService<EmailMessage>, IEmailMessageService
    {
        public EmailMessageService(AppDbContext context, IMapper mapper = null)
            : base(context, mapper)
        {
        }

        public async Task<EmailMessage> AddAsync(EmailDto emailDTO, string shortName)
        {
            EmailMessage emailMessage = Mapper.Map<EmailMessage>(emailDTO);
            emailMessage.NotificationType = shortName;
            Insert(emailMessage);
            await Context.SaveChangesAsync();

            return emailMessage;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            EmailMessage email = await GetByIdAsync(id);
            Delete(email);
            await Context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmailMessage>> GetAllAsync()
        {
            return await Entities.ToListAsync();
        }

        public async Task<EmailMessage> GetByIdAsync(Guid id)
        {
            return await Context.EmailMessages.AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task<EmailMessage> GetByNotificationTypeAsync(string notificationType)
        {
            return await Entities.FirstAsync(e => e.NotificationType.Equals(notificationType));
        }

        public string PerformReplacement(string text, Dictionary<string, string> pattern)
        {
            foreach (var element in pattern)
            {
                text = text.Replace(element.Key, element.Value);
            }

            return text;
        }

        public async Task<EmailMessage> UpdateAsync(Guid id, EmailDto emailDTO, string shortName)
        {
            EmailMessage emailMessage = Mapper.Map<EmailMessage>(emailDTO);
            emailMessage.Id = id;
            emailMessage.NotificationType = shortName;
            emailMessage = Update(emailMessage);
            await Context.SaveChangesAsync();

            return emailMessage;
        }
    }
}
