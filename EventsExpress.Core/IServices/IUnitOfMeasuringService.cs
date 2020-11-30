using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;

namespace EventsExpress.Core.IServices
{
    public interface IUnitOfMeasuringService
    {
        Task<Guid> Create(UnitOfMeasuringDTO unitOfMeasuringDTO);

        Task<Guid> Edit(UnitOfMeasuringDTO unitOfMeasuringDTO);

        ICollection<UnitOfMeasuringDTO> GetAll();

        UnitOfMeasuringDTO GetById(Guid unitOfMeasuringId);
    }
}
