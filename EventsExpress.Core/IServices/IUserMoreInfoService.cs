using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;

namespace EventsExpress.Core.IServices
{
    public interface IUserMoreInfoService
    {
        Task<Guid> CreateAsync(UserMoreInfoDto userMoreInfoDto);
    }
}
