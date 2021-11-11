namespace EventsExpress.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using EventsExpress.Core.DTOs;
    using EventsExpress.Core.Exceptions;
    using EventsExpress.Core.IServices;
    using EventsExpress.Db.EF;
    using EventsExpress.Db.Entities;

    public class UserMoreInfoService : BaseService<UserMoreInfo> // , IUserMoreInfoService
    {
        public UserMoreInfoService(
            AppDbContext context,
            IMapper mapper)
            : base(context, mapper)
        {
        }

        /*public IEnumerable<UnitOfMeasuringDto> GetAll()
        {
            return Mapper.Map<IEnumerable<UnitOfMeasuring>, IEnumerable<UnitOfMeasuringDto>>(
               Context.UnitOfMeasurings.Include(c => c.Category)
                                       .Where(item => !item.IsDeleted)
                                       .OrderBy(unit => unit.Category.CategoryName)
                                       .ThenBy(unit => unit.UnitName)
                                       .ThenBy(unit => unit.ShortName));
        }*/

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

       /* public bool ExistsByItems(string unitName, string shortName, Guid categoryId)
        {
            return Context.UnitOfMeasurings
                  .Include(e => e.Category)
                  .Any(x => (!x.IsDeleted) && (x.UnitName == unitName)
                                           && (x.ShortName == shortName)
                                           && (x.CategoryId == categoryId));
        }*/

        public async Task<Guid> Create(UserMoreInfoDTO userMoreInfoDTO)
        {
            if (userMoreInfoDTO == null)
            {
                throw new EventsExpressException("Null object");
            }
            else
            {
                var result = Insert(Mapper.Map<UserMoreInfoDTO, UserMoreInfo>(userMoreInfoDTO));
                await Context.SaveChangesAsync();
                return result.Id;
            }
        }

        /*public async Task<Guid> Edit(UserMoreInfoDTO userMoreInfoDTO)
        {
            var entity = Context.UserMoreInfos
                .Include(e => e.Category)
                .FirstOrDefault(x => x.Id == unitOfMeasuringDTO.Id);
            if (entity == null || entity.IsDeleted)
            {
                throw new EventsExpressException("Object not found");
            }

            entity.ShortName = unitOfMeasuringDTO.ShortName;
            entity.UnitName = unitOfMeasuringDTO.UnitName;
            entity.CategoryId = unitOfMeasuringDTO.Category.Id;
            await Context.SaveChangesAsync();

            return entity.Id;
        }*/

        /*IEnumerable<UserMoreInfoDTO> IUserMoreInfoService.GetAll()
        {
            throw new NotImplementedException();
        }

        UserMoreInfoDTO IUserMoreInfoService.GetById(Guid userMoreInfoId)
        {
            throw new NotImplementedException();
        }

        Task IUserMoreInfoService.Delete(Guid id)
        {
            throw new NotImplementedException();
        }*/
    }
}
