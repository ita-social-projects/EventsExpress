using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<OperationResult> Create(UnitOfMeasuringDTO unitOfMeasuringDTO)
        {
            try
            {
                var result = _db.UnitOfMeasuringRepository.Insert(_mapper.Map<UnitOfMeasuringDTO, UnitOfMeasuring>(unitOfMeasuringDTO));
                await _db.SaveAsync();
                return new OperationResult(true, "Unit of measuring was created!", result.Id.ToString());
            }
            catch (Exception ex)
            {
                return new OperationResult(false, "Something went wrong " + ex.Message, string.Empty);
            } 
        }

        public async Task<OperationResult> Delete(Guid unitOfMeasuringId)
        {
            if (unitOfMeasuringId == Guid.Empty)
            {
                return new OperationResult(false, "Id field is '0'", string.Empty);
            }

            var entity = _db.UnitOfMeasuringRepository.Get(unitOfMeasuringId);
            if (entity == null)
            {
                return new OperationResult(false, "Not found", string.Empty);
            }

            var result = _db.UnitOfMeasuringRepository.Delete(entity);
            await _db.SaveAsync();

            if (result != null)
            {
                return new OperationResult(true);
            }

            return new OperationResult(false, "Error!", string.Empty);
        }

        public async Task<OperationResult> Edit(UnitOfMeasuringDTO unitOfMeasuringDTO)
        {
            var entity = _db.UnitOfMeasuringRepository.Get(unitOfMeasuringDTO.Id);
            if (entity == null)
            {
                return new OperationResult(false, "Object not found", unitOfMeasuringDTO.Id.ToString());
            }

            try
            {
                entity.ShortName = unitOfMeasuringDTO.ShortName;
                entity.UnitName = unitOfMeasuringDTO.UnitName;
                await _db.SaveAsync();
                return new OperationResult(true, "Eidt unit of measuring", entity.Id.ToString());
            }
            catch (Exception ex)
            {
                return new OperationResult(false, "Something went wrong " + ex.Message, string.Empty);
            }
        }

        public ICollection<UnitOfMeasuringDTO> GetAll()
        {
            var entities = _db.UnitOfMeasuringRepository.Get(string.Empty).ToList();
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
                //throw new NotImplementedException("Object not found!");
                return new UnitOfMeasuringDTO();
            }

            return _mapper.Map<UnitOfMeasuring, UnitOfMeasuringDTO>(entity);
        }
    }
}
