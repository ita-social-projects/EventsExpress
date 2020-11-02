using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var ev = _db.EventRepository.Get("Inventories").FirstOrDefault(e => e.Id == eventId);
            if (ev == null)
            {
                return new OperationResult(false, "Event not found!", eventId.ToString());
            }

            try
            {
                var entity = _mapper.Map<InventoryDTO, Inventory>(inventoryDTO);
                entity.EventId = eventId;
                //entity.UnitOfMeasuringId = new Guid("f298634e-56e9-420d-7a33-08d87b70947d"); very important property, it's can't be null!
                var result = _db.InventoryRepository.Insert(entity);
                await _db.SaveAsync();
                return new OperationResult(true, "Invertar was added", result.Id.ToString());
            }
            catch (Exception ex)
            {
                return new OperationResult(false, ex.Message, string.Empty);
            }
        }

        public IEnumerable<InventoryDTO> GetInventar(Guid eventId)
        {
            return _mapper.Map<IEnumerable<InventoryDTO>>(_db.InventoryRepository.Get("UnitOfMeasuring").Where(i => i.EventId == eventId));
        }
    }
}
