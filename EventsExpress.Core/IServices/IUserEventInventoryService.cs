using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;

namespace EventsExpress.Core.IServices
{
    public interface IUserEventInventoryService
    {
        Task MarkItemAsTakenByUser(UserEventInventoryDto userEventInventoryDTO);

        Task<IEnumerable<UserEventInventoryDto>> GetAllMarkItemsByEventId(Guid eventId);

        Task Delete(UserEventInventoryDto userEventInventoryDTO);

        Task Edit(UserEventInventoryDto userEventInventoryDTO);
    }
}
