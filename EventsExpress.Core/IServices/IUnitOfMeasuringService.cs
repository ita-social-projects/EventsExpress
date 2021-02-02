using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;

namespace EventsExpress.Core.IServices
{
    public interface IUnitOfMeasuringService
    {
        Task<Guid> Create(UnitOfMeasuringDto unitOfMeasuringDTO);

        Task<Guid> Edit(UnitOfMeasuringDto unitOfMeasuringDTO);

        IEnumerable<UnitOfMeasuringDto> GetAll();

        UnitOfMeasuringDto GetById(Guid unitOfMeasuringId);

        Task Delete(Guid id);

        bool ExistsByName(string unitName, string shortName);
    }
}
