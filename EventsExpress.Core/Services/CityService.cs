using System;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;

namespace EventsExpress.Core.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork _db;

        public CityService(IUnitOfWork uow)
        {
            _db = uow;
        }

        public IQueryable<City> GetCitiesByCountryId(Guid id) => _db.CityRepository.Get().Where(c => c.CountryId == id);

        public IQueryable<City> GetAll() => _db.CityRepository.Get();

        public City Get(Guid id) => _db.CityRepository.Get(id);

        public async Task CreateCityAsync(City city)
        {
            var country = _db.CountryRepository.Get(city.CountryId);

            if (country == null)
            {
                throw new EventsExpressException($"Bad country Id: {city.CountryId}");
            }

            city.Country = country;

            var result = GetCitiesByCountryId(city.Country.Id)
                .Any(c => string.Equals(c.Name, city.Name, StringComparison.CurrentCultureIgnoreCase));
            if (result)
            {
                throw new EventsExpressException("City is already exist!");
            }

            _db.CityRepository.Insert(city);
            await _db.SaveAsync();
        }

        public async Task EditCityAsync(City city)
        {
            var country = _db.CountryRepository.Get(city.CountryId);
            if (country == null)
            {
                throw new EventsExpressException($"Bad country Id: {city.CountryId}");
            }

            city.Country = country;

            var oldCity = _db.CityRepository.Get(city.Id);
            if (oldCity == null)
            {
                throw new EventsExpressException("Not found");
            }

            oldCity.Name = city.Name;
            oldCity.CountryId = city.CountryId;
            await _db.SaveAsync();
        }

        public async Task DeleteCityAsync(Guid id)
        {
            var city = _db.CityRepository.Get(id);
            if (city == null)
            {
                throw new EventsExpressException("Not found");
            }

            _db.CityRepository.Delete(city);
            await _db.SaveAsync();
        }
    }
}
