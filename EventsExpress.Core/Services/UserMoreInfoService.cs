using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Bridge;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;

namespace EventsExpress.Core.Services;

public class UserMoreInfoService : BaseService<UserMoreInfo>, IUserMoreInfoService
{
    private readonly ISecurityContext _securityContext;

    public UserMoreInfoService(AppDbContext context, IMapper mapper, ISecurityContext securityContext)
        : base(context, mapper)
    {
        _securityContext = securityContext;
    }

    public async Task<Guid> CreateAsync(UserMoreInfoDto userMoreInfoDto)
    {
        userMoreInfoDto.UserId = _securityContext.GetCurrentUserId();
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

        await Context.SaveChangesAsync();
        return newUserMoreInfo.Id;
    }
}
