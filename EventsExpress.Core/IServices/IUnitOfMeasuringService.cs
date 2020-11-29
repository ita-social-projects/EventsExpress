using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;

namespace EventsExpress.Core.IServices
{
    public interface IUnitOfMeasuringService
    {
        Task<OperationResult> Create(UnitOfMeasuringDTO unitOfMeasuringDTO);

        Task<OperationResult> Edit(UnitOfMeasuringDTO unitOfMeasuringDTO);

        IEnumerable<UnitOfMeasuringDTO> GetAll();

        UnitOfMeasuringDTO GetById(Guid unitOfMeasuringId);
    }
}
