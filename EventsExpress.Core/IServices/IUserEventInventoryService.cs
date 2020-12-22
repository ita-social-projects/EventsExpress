using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;

namespace EventsExpress.Core.IServices
{
    public interface IUserEventInventoryService
    {
        Task MarkItemAsTakenByUser(UserEventInventoryDTO userEventInventoryDTO);

        Task<IEnumerable<UserEventInventoryDTO>> GetAllMarkItemsByEventId(Guid eventId);

        Task Delete(UserEventInventoryDTO userEventInventoryDTO);

        Task Edit(UserEventInventoryDTO userEventInventoryDTO);
    }
}
