using System;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;

namespace EventsExpress.Core.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork db;

        public CityService(IUnitOfWork uow)
        {
            db = uow;
        }

        public IQueryable<City> GetCitiesByCountryId(Guid id) => db.CityRepository.Get().Where(c => c.CountryId == id);

        public IQueryable<City> GetAll() => db.CityRepository.Get();

        public City Get(Guid id) => db.CityRepository.Get(id);

        public async Task<OperationResult> CreateCityAsync(City city)
        {
            var country = db.CountryRepository.Get(city.CountryId);

            if (country == null)
            {
                return new OperationResult(false, $"Bad country Id: {city.CountryId}", string.Empty);
            }

            city.Country = country;

            var result = GetCitiesByCountryId(city.Country.Id)
                .Any(c => string.Equals(c.Name, city.Name, StringComparison.CurrentCultureIgnoreCase));
            if (result)
            {
                return new OperationResult(false, "City is already exist!", string.Empty);
            }

            db.CityRepository.Insert(city);
            await db.SaveAsync();

            return new OperationResult(true);
        }

        public async Task<OperationResult> EditCityAsync(City city)
        {
            var country = db.CountryRepository.Get(city.CountryId);
            if (country == null)
            {
                return new OperationResult(false, $"Bad country Id: {city.CountryId}", string.Empty);
            }

            city.Country = country;

            var oldCity = db.CityRepository.Get(city.Id);
            if (oldCity == null)
            {
                return new OperationResult(false, "Not found", string.Empty);
            }

            oldCity.Name = city.Name;
            oldCity.CountryId = city.CountryId;
            await db.SaveAsync();

            return new OperationResult(true);
        }

        public async Task<OperationResult> DeleteCityAsync(Guid id)
        {
            var city = db.CityRepository.Get(id);
            if (city == null)
            {
                return new OperationResult(false, "Not found", string.Empty);
            }

            db.CityRepository.Delete(city);
            await db.SaveAsync();
            return new OperationResult(true);
        }
    }
}
