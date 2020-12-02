using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Db.BaseService;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;

namespace EventsExpress.Core.Services
{
    public class UnitOfMeasuringService : BaseService<UnitOfMeasuring>, IUnitOfMeasuringService
    {
        public UnitOfMeasuringService(
            AppDbContext context,
            IMapper mapper)
            : base(context, mapper)
        {
        }

        public async Task<Guid> Create(UnitOfMeasuringDTO unitOfMeasuringDTO)
        {
            var result = Insert(_mapper.Map<UnitOfMeasuringDTO, UnitOfMeasuring>(unitOfMeasuringDTO));
            await _context.SaveChangesAsync();

            return result.Id;
        }

        public async Task<Guid> Edit(UnitOfMeasuringDTO unitOfMeasuringDTO)
        {
            var entity = _context.UnitOfMeasurings.Find(unitOfMeasuringDTO.Id);
            if (entity == null)
            {
                throw new EventsExpressException("Object not found");
            }

            entity.ShortName = unitOfMeasuringDTO.ShortName;
            entity.UnitName = unitOfMeasuringDTO.UnitName;
            await _context.SaveChangesAsync();

            return entity.Id;
        }

        public IEnumerable<UnitOfMeasuringDTO> GetAll()
        {
            var entities = _context.UnitOfMeasurings.AsEnumerable();

            return _mapper.Map<IEnumerable<UnitOfMeasuring>, IEnumerable<UnitOfMeasuringDTO>>(entities);
        }

        public UnitOfMeasuringDTO GetById(Guid unitOfMeasuringId)
        {
            var entity = _context.UnitOfMeasurings.Find(unitOfMeasuringId);

            return _mapper.Map<UnitOfMeasuring, UnitOfMeasuringDTO>(entity);
        }
    }
}
