using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;

namespace EventsExpress.Core.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;

        public InventoryService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _db = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OperationResult> AddInventar(Guid eventId, InventoryDTO inventoryDTO)
        {
            var ev = _db.EventRepository.Get().FirstOrDefault(e => e.Id == eventId);
            if (ev == null)
            {
                return new OperationResult(false, "Event not found!", eventId.ToString());
            }

            try
            {
                var entity = _mapper.Map<InventoryDTO, Inventory>(inventoryDTO);
                entity.EventId = eventId;
                var result = _db.InventoryRepository.Insert(entity);
                await _db.SaveAsync();
                return new OperationResult(true, "Invertar was added", result.Id.ToString());
            }
            catch (Exception ex)
            {
                return new OperationResult(false, ex.Message, string.Empty);
            }
        }

        public async Task<OperationResult> EditInventar(InventoryDTO inventoryDTO)
        {
            var entity = _db.InventoryRepository.Get(inventoryDTO.Id);
            if (entity == null)
            {
                return new OperationResult(false, "Object not found", inventoryDTO.Id.ToString());
            }

            try
            {
                entity.ItemName = inventoryDTO.ItemName;
                entity.NeedQuantity = inventoryDTO.NeedQuantity;
                entity.UnitOfMeasuringId = inventoryDTO.UnitOfMeasuring.Id;
                await _db.SaveAsync();
                return new OperationResult(true, "Edit inventory", entity.Id.ToString());
            }
            catch (Exception ex)
            {
                return new OperationResult(false, "Something went wrong " + ex.Message, string.Empty);
            }
        }

        public IEnumerable<InventoryDTO> GetInventar(Guid eventId)
        {
            var ev = _db.EventRepository.Get(eventId);
            if (ev == null)
            {
                return new List<InventoryDTO>();
            }

            return _mapper.Map<IEnumerable<InventoryDTO>>(_db.InventoryRepository.Get("UnitOfMeasuring").Where(i => i.EventId == eventId));
        }

        public InventoryDTO GetInventarById(Guid inventoryId)
        {
            var entity = _db.InventoryRepository.Get(inventoryId);
            if (entity == null)
            {
                return new InventoryDTO();
            }

            return _mapper.Map<Inventory, InventoryDTO>(entity);
        }
    }
}
