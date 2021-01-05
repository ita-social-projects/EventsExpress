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
            var isExistedUnit = _context.UnitOfMeasurings.FirstOrDefault(x => x.IsDeleted && (x.UnitName == unitOfMeasuringDTO.UnitName) && (x.ShortName == unitOfMeasuringDTO.ShortName));
            Guid resId;

            if (isExistedUnit == null)
            {
                var result = Insert(_mapper.Map<UnitOfMeasuringDTO, UnitOfMeasuring>(unitOfMeasuringDTO));
                resId = result.Id;
            }
            else
            {
                isExistedUnit.IsDeleted = false;
                resId = isExistedUnit.Id;
            }

            await _context.SaveChangesAsync();

            return resId;
        }
        //public async Task Create(string unitName, string shortName)
        //{
        //    Insert(new UnitOfMeasuring { UnitName = unitName, ShortName = shortName });
        //    await _context.SaveChangesAsync();
        //}

        public async Task<Guid> Edit(UnitOfMeasuringDTO unitOfMeasuringDTO)
        {
            var entity = _context.UnitOfMeasurings.Find(unitOfMeasuringDTO.Id);
            if (entity == null || entity.IsDeleted)
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
            var entities = _context.UnitOfMeasurings.Where(item => !item.IsDeleted).AsEnumerable();

            return _mapper.Map<IEnumerable<UnitOfMeasuring>, IEnumerable<UnitOfMeasuringDTO>>(entities);
        }

        public UnitOfMeasuringDTO GetById(Guid unitOfMeasuringId)
        {
            var entity = _context.UnitOfMeasurings.FirstOrDefault(item => !item.IsDeleted && item.Id == unitOfMeasuringId);
            if (entity != null)
            {
                return _mapper.Map<UnitOfMeasuring, UnitOfMeasuringDTO>(entity);
            }

            throw new EventsExpressException("Not found");

            //var entity = _context.UnitOfMeasurings.Find(unitOfMeasuringId);
            //if (!entity.IsDeleted)
            //{
            //    return _mapper.Map<UnitOfMeasuring, UnitOfMeasuringDTO>(entity);
            //}

            //throw new EventsExpressException("Not found");

        }

        public async Task Delete(Guid id)
        {
            var unitOfMeasuring = _context.UnitOfMeasurings.Find(id);
            if (unitOfMeasuring == null || unitOfMeasuring.IsDeleted)
            {
                return;
            }

            unitOfMeasuring.IsDeleted = true;

            await _context.SaveChangesAsync();
        }
        
        public bool Exists(Guid id)
        {
           return _context.UnitOfMeasurings.Any(x => x.Id == id);
        }

        public bool ExistsByName(string unitName, string shortName)
        {
          return _context.UnitOfMeasurings.Any(x => (!x.IsDeleted) && (x.UnitName == unitName) && (x.ShortName == shortName));
        }

        public bool ExistsAll(IEnumerable<Guid> ids)
        {
           return _context.UnitOfMeasurings.Count(x => ids.Contains(x.Id)) == ids.Count();
        }
    }
}
