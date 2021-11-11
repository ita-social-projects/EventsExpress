using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;

namespace EventsExpress.Core.IServices
{
    public interface IUserMoreInfoService
    {
        Task<Guid> Create(UserMoreInfoDTO userMoreInfoDTO);

        Task<Guid> Edit(UserMoreInfoDTO userMoreInfoDTO);

        IEnumerable<UserMoreInfoDTO> GetAll();

        UserMoreInfoDTO GetById(Guid userMoreInfoId);

        Task Delete(Guid id);
    }
}
