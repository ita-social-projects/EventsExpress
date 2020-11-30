using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
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

        public async Task<Guid> AddInventar(Guid eventId, InventoryDTO inventoryDTO)
        {
            var ev = _db.EventRepository.Get().FirstOrDefault(e => e.Id == eventId);
            if (ev == null)
            {
                throw new EventsExpressException("Event not found!");
            }

            try
            {
                var entity = _mapper.Map<InventoryDTO, Inventory>(inventoryDTO);
                entity.EventId = eventId;
                var result = _db.InventoryRepository.Insert(entity);
                await _db.SaveAsync();
                return result.Id;
            }
            catch (Exception ex)
            {
                throw new EventsExpressException(ex.Message);
            }
        }

        public async Task<Guid> EditInventar(InventoryDTO inventoryDTO)
        {
            var entity = _db.InventoryRepository.Get(inventoryDTO.Id);
            if (entity == null)
            {
                throw new EventsExpressException("Object not found");
            }

            try
            {
                entity.ItemName = inventoryDTO.ItemName;
                entity.NeedQuantity = inventoryDTO.NeedQuantity;
                entity.UnitOfMeasuringId = inventoryDTO.UnitOfMeasuring.Id;
                await _db.SaveAsync();
                return entity.Id;
            }
            catch (Exception ex)
            {
                throw new EventsExpressException(ex.Message);
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
