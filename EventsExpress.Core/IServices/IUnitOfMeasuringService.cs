using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventsExpress.Core.IServices
{
    public interface IUnitOfMeasuringService
    {
        Task<OperationResult> Create(UnitOfMeasuringDTO unitOfMeasuringDTO);

        Task<OperationResult> Edit(UnitOfMeasuringDTO unitOfMeasuringDTO);

        Task<OperationResult> Delete(Guid unitOfMeasuringId);

        ICollection<UnitOfMeasuringDTO> GetAll();

        UnitOfMeasuringDTO GetById(Guid unitOfMeasuringId);
    }
}
