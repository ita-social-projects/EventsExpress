using EventsExpress.Core.Infrastructure;
using EventsExpress.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
