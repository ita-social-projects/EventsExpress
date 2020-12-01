using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventsExpress.Core.IServices
{
    public interface IUserEventInventoryService
    {
        Task<OperationResult> MarkItemAsTakenByUser(UserEventInventoryDTO userEventInventoryDTO);

        IEnumerable<UserEventInventoryDTO> GetAllMarksByItemId(Guid itemId);
    }
}
