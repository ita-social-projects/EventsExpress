using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Db.Entities;

namespace EventsExpress.Core.IServices
{
    public interface ICityService
    {
        City GetById(Guid id);

        IEnumerable<City> GetCitiesByCountryId(Guid id);

        IEnumerable<City> GetAll();

        Task<OperationResult> CreateCityAsync(City city);

        Task<OperationResult> EditCityAsync(City city);

        Task<OperationResult> DeleteCityAsync(Guid id);
    }
}
