using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventsExpress.Core.IServices
{
    public interface IUserEventInventoryService
    {
        Task MarkItemAsTakenByUser(UserEventInventoryDTO userEventInventoryDTO);

        IEnumerable<UserEventInventoryDTO> GetAllMarkItemsByEventId(Guid eventId);
    }
}
