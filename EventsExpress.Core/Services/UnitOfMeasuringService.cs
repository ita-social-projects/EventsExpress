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

        public async Task<Guid> Create(UnitOfMeasuringDto unitOfMeasuringDTO)
        {
            if (unitOfMeasuringDTO == null)
            {
                throw new EventsExpressException("Null object");
            }
            else
            {
                var result = Insert(Mapper.Map<UnitOfMeasuringDto, UnitOfMeasuring>(unitOfMeasuringDTO));
                await Context.SaveChangesAsync();
                return result.Id;
            }
        }

        public async Task<Guid> Edit(UnitOfMeasuringDto unitOfMeasuringDTO)
        {
            var entity = Context.UnitOfMeasurings.Find(unitOfMeasuringDTO.Id);
            if (entity == null || entity.IsDeleted)
            {
                throw new EventsExpressException("Object not found");
            }

            entity.ShortName = unitOfMeasuringDTO.ShortName;
            entity.UnitName = unitOfMeasuringDTO.UnitName;
            await Context.SaveChangesAsync();

            return entity.Id;
        }

        public IEnumerable<UnitOfMeasuringDto> GetAll() => Mapper.Map<IEnumerable<UnitOfMeasuring>, IEnumerable<UnitOfMeasuringDto>>(
                    Context.UnitOfMeasurings.Where(item => !item.IsDeleted)
                                            .OrderBy(unit => unit.UnitName)
                                            .ThenBy(unit => unit.ShortName));

        public UnitOfMeasuringDto GetById(Guid unitOfMeasuringId)
        {
            var unitOfMeasuring = Context.UnitOfMeasurings.Find(unitOfMeasuringId);
            if (unitOfMeasuring == null || unitOfMeasuring.IsDeleted)
            {
                throw new EventsExpressException("Not found");
            }

            return new UnitOfMeasuringDto
            {
                Id = unitOfMeasuring.Id,
                UnitName = unitOfMeasuring.UnitName,
                ShortName = unitOfMeasuring.ShortName,
                IsDeleted = unitOfMeasuring.IsDeleted,
            };
        }

        public async Task Delete(Guid id)
        {
            var unitOfMeasuring = Context.UnitOfMeasurings.Find(id);
            if (unitOfMeasuring == null || unitOfMeasuring.IsDeleted)
            {
                return;
            }

            unitOfMeasuring.IsDeleted = true;

            await Context.SaveChangesAsync();
        }

        public bool ExistsByName(string unitName, string shortName)
        {
          return Context.UnitOfMeasurings.Any(x => ((!x.IsDeleted) && (x.UnitName == unitName) && (x.ShortName == shortName)));
        }
    }
}
