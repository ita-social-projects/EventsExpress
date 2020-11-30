using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;

namespace EventsExpress.Core.Services
{
    public class UnitOfMeasuringService : IUnitOfMeasuringService
    {
        private readonly IUnitOfWork _db;
        private readonly IMapper _mapper;

        public UnitOfMeasuringService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _db = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> Create(UnitOfMeasuringDTO unitOfMeasuringDTO)
        {
            try
            {
                var result = _db.UnitOfMeasuringRepository.Insert(_mapper.Map<UnitOfMeasuringDTO, UnitOfMeasuring>(unitOfMeasuringDTO));
                await _db.SaveAsync();
                return result.Id;
            }
            catch (Exception ex)
            {
                throw new EventsExpressException(ex.Message);
            }
        }

        public async Task<Guid> Edit(UnitOfMeasuringDTO unitOfMeasuringDTO)
        {
            var entity = _db.UnitOfMeasuringRepository.Get(unitOfMeasuringDTO.Id);
            if (entity == null)
            {
                throw new EventsExpressException("Object not found");
            }

            try
            {
                entity.ShortName = unitOfMeasuringDTO.ShortName;
                entity.UnitName = unitOfMeasuringDTO.UnitName;
                await _db.SaveAsync();
                return entity.Id;
            }
            catch (Exception ex)
            {
                throw new EventsExpressException(ex.Message);
            }
        }

        public ICollection<UnitOfMeasuringDTO> GetAll()
        {
            var entities = _db.UnitOfMeasuringRepository.Get().ToList();
            if (entities == null)
            {
                return new List<UnitOfMeasuringDTO>();
            }
            else
            {
                return _mapper.Map<ICollection<UnitOfMeasuring>, ICollection<UnitOfMeasuringDTO>>(entities);
            }
        }

        public UnitOfMeasuringDTO GetById(Guid unitOfMeasuringId)
        {
            var entity = _db.UnitOfMeasuringRepository.Get(unitOfMeasuringId);
            if (entity == null)
            {
                return new UnitOfMeasuringDTO();
            }

            return _mapper.Map<UnitOfMeasuring, UnitOfMeasuringDTO>(entity);
        }
    }
}
