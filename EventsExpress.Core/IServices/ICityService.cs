using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Db.Entities;

namespace EventsExpress.Core.IServices
{
    public interface ICityService
    {
        City GetById(Guid id);

        IEnumerable<City> GetCitiesByCountryId(Guid id);

        IEnumerable<City> GetAll();

        Task CreateCityAsync(City city);

        Task EditCityAsync(City city);

        Task DeleteCityAsync(Guid id);
    }
}
