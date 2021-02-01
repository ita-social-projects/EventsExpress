using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;

namespace EventsExpress.Core.IServices
{
    public interface IInventoryService
    {
        Task<Guid> AddInventar(Guid eventId, InventoryDto inventoryDTO);

        Task<Guid> EditInventar(InventoryDto inventoryDTO);

        Task<Guid> DeleteInventar(Guid id);

        IEnumerable<InventoryDto> GetInventar(Guid eventId);

        InventoryDto GetInventarById(Guid inventoryId);
    }
}
