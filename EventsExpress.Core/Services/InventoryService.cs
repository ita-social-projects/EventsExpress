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

namespace EventsExpress.Core.Services
{
    public class InventoryService : BaseService<Inventory>, IInventoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public InventoryService(
            AppDbContext context,
            IMapper mapper)
            : base(context)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<OperationResult> EditInventar(InventoryDTO inventoryDTO)
        {
            var entity = Get(inventoryDTO.Id);
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
            var ev = _context.Events.Where(x => x.Id == eventId);
            if (ev == null)
            {
                return new List<InventoryDTO>();
            }

            return _mapper.Map<IEnumerable<InventoryDTO>>(Get("UnitOfMeasuring").Where(i => i.EventId == eventId));
        }

        public InventoryDTO GetInventarById(Guid inventoryId)
        {
            var entity = Get(inventoryId);
            if (entity == null)
            {
                return new InventoryDTO();
            }

            return _mapper.Map<Inventory, InventoryDTO>(entity);
        }
    }
}
