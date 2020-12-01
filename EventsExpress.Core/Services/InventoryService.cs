using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.BaseService;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services
{
    public class InventoryService : BaseService<Inventory>, IInventoryService
    {
        public InventoryService(
            AppDbContext context,
            IMapper mapper)
            : base(context, mapper)
        {
        }

        public async Task<OperationResult> AddInventar(Guid eventId, InventoryDTO inventoryDTO)
        {
            var ev = _context.Events.FirstOrDefault(e => e.Id == eventId);
            if (ev == null)
            {
                return new OperationResult(false, "Event not found!", eventId.ToString());
            }

            try
            {
                var entity = _mapper.Map<InventoryDTO, Inventory>(inventoryDTO);
                entity.EventId = eventId;
                var result = Insert(entity);
                await _context.SaveChangesAsync();
                return new OperationResult(true, "Invertar was added", result.Id.ToString());
            }
            catch (Exception ex)
            {
                return new OperationResult(false, ex.Message, string.Empty);
            }
        }

        public async Task<OperationResult> DeleteInventar(Guid id)
        {
            var inventar = _context.Inventories.Find(id);
            if (inventar == null)
            {
                return new OperationResult(false, "Not found", string.Empty);
            }

            try
            {
                var result = _context.Inventories.Remove(inventar);
                await _context.SaveChangesAsync();
                return new OperationResult(true, "Inventar was deleted", inventar.Id.ToString());
            }
            catch (Exception ex)
            {
                return new OperationResult(false, "Something went wrong " + ex.Message, string.Empty);
            }
        }

        public async Task<OperationResult> EditInventar(InventoryDTO inventoryDTO)
        {
            var entity = _context.Inventories.Find(inventoryDTO.Id);
            if (entity == null)
            {
                return new OperationResult(false, "Object not found", inventoryDTO.Id.ToString());
            }

            try
            {
                entity.ItemName = inventoryDTO.ItemName;
                entity.NeedQuantity = inventoryDTO.NeedQuantity;
                entity.UnitOfMeasuringId = inventoryDTO.UnitOfMeasuring.Id;
                await _context.SaveChangesAsync();
                return new OperationResult(true, "Edit inventory", entity.Id.ToString());
            }
            catch (Exception ex)
            {
                return new OperationResult(false, "Something went wrong " + ex.Message, string.Empty);
            }
        }

        public IEnumerable<InventoryDTO> GetInventar(Guid eventId)
        {
            if (!_context.Events.Any(x => x.Id == eventId))
            {
                return new List<InventoryDTO>();
            }

            return _mapper.Map<IEnumerable<InventoryDTO>>(
                _context.Inventories
                    .Include(i => i.UnitOfMeasuring)
                    .Where(i => i.EventId == eventId));
        }

        public InventoryDTO GetInventarById(Guid inventoryId)
        {
            var entity = _context.Inventories.Find(inventoryId);

            if (entity == null)
            {
                return new InventoryDTO();
            }

            return _mapper.Map<Inventory, InventoryDTO>(entity);
        }
    }
}
