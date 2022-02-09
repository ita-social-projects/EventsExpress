namespace EventsExpress.Core.Services
{
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

    public class UserMoreInfoService : BaseService<UserMoreInfo>, IUserMoreInfoService
    {
        public UserMoreInfoService(
            AppDbContext context,
            IMapper mapper)
            : base(context, mapper)
        {
        }

        public async Task<Guid> CreateAsync(UserMoreInfoDto userMoreInfoDto)
        {
            if (Context.UserMoreInfo.Any(u => u.UserId == userMoreInfoDto.UserId))
            {
                throw new EventsExpressException("Additional info already exists about this user");
            }

            var userMoreInfo = Mapper.Map<UserMoreInfo>(userMoreInfoDto);
            var newUserMoreInfo = Insert(userMoreInfo);
            if (newUserMoreInfo.Id == Guid.Empty)
            {
                throw new EventsExpressException("Inserting failed");
            }

            var user = await Context.Users.FindAsync(userMoreInfo.UserId);
            if (user is not null)
            {
                user.UserMoreInfo = newUserMoreInfo;
            }

            await Context.SaveChangesAsync();
            return newUserMoreInfo.Id;
        }
    }
}
