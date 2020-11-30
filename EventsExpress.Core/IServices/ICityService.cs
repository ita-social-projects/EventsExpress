using System;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Db.Entities;

namespace EventsExpress.Core.IServices
{
    public interface ICityService
    {
        City Get(Guid id);

        IQueryable<City> GetCitiesByCountryId(Guid id);

        IQueryable<City> GetAll();

        Task CreateCityAsync(City city);

        Task EditCityAsync(City city);

        Task DeleteCityAsync(Guid id);
    }
}
