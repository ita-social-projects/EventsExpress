using System;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Db.Entities;

namespace EventsExpress.Core.IServices
{
    public interface ICityService
    {
        City Get(Guid id);

        IQueryable<City> GetCitiesByCountryId(Guid id);

        IQueryable<City> GetAll();

        Task<OperationResult> CreateCityAsync(City city);

        Task<OperationResult> EditCityAsync(City city);

        Task<OperationResult> DeleteCityAsync(Guid id);
    }
}
