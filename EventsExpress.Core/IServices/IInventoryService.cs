using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;

namespace EventsExpress.Core.IServices
{
    public interface IInventoryService
    {
        Task<Guid> AddInventar(Guid eventId, InventoryDTO inventoryDTO);

        Task<Guid> EditInventar(InventoryDTO inventoryDTO);

        Task<Guid> DeleteInventar(Guid id);

        IEnumerable<InventoryDTO> GetInventar(Guid eventId);

        InventoryDTO GetInventarById(Guid inventoryId);
    }
}
