using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class UnitOfMeasuringService : BaseService<UnitOfMeasuring>, IUnitOfMeasuringService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UnitOfMeasuringService(
            AppDbContext context,
            IMapper mapper)
            : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<OperationResult> Create(UnitOfMeasuringDTO unitOfMeasuringDTO)
        {
            try
            {
                var result = Insert(_mapper.Map<UnitOfMeasuringDTO, UnitOfMeasuring>(unitOfMeasuringDTO));
                await _context.SaveChangesAsync();
                return new OperationResult(true, "Unit of measuring was created!", result.Id.ToString());
            }
            catch (Exception ex)
            {
                return new OperationResult(false, "Something went wrong " + ex.Message, string.Empty);
            }
        }

        public async Task<OperationResult> Edit(UnitOfMeasuringDTO unitOfMeasuringDTO)
        {
            var entity = Get(unitOfMeasuringDTO.Id);
            if (entity == null)
            {
                return new OperationResult(false, "Object not found", unitOfMeasuringDTO.Id.ToString());
            }

            try
            {
                entity.ShortName = unitOfMeasuringDTO.ShortName;
                entity.UnitName = unitOfMeasuringDTO.UnitName;
                await _context.SaveChangesAsync();
                return new OperationResult(true, "Edit unit of measuring", entity.Id.ToString());
            }
            catch (Exception ex)
            {
                return new OperationResult(false, "Something went wrong " + ex.Message, string.Empty);
            }
        }

        public ICollection<UnitOfMeasuringDTO> GetAll()
        {
            var entities = Get().ToList();
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
            var entity = Get(unitOfMeasuringId);
            if (entity == null)
            {
                return new UnitOfMeasuringDTO();
            }

            return _mapper.Map<UnitOfMeasuring, UnitOfMeasuringDTO>(entity);
        }
    }
}
