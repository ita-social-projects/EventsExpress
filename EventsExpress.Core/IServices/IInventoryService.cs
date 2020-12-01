using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;

namespace EventsExpress.Core.IServices
{
    public interface IInventoryService
    {
        Task<OperationResult> AddInventar(Guid eventId, InventoryDTO inventoryDTO);

        Task<OperationResult> EditInventar(InventoryDTO inventoryDTO);

        Task<OperationResult> DeleteInventar(Guid id);

        IEnumerable<InventoryDTO> GetInventar(Guid eventId);

        InventoryDTO GetInventarById(Guid inventoryId);
    }
}
