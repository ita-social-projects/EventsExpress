using System;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventsExpress.Core.IServices
{
    public interface IInventoryService
    {
        Task<OperationResult> AddInventar(Guid eventId, InventoryDTO inventoryDTO);

        IEnumerable<InventoryDTO> GetInventar(Guid eventId);
    }
}
