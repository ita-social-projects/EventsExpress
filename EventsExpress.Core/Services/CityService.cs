using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.Core.Services
{
    public class CityService : ICityService
    {
        private readonly IUnitOfWork Db;

        public CityService(IUnitOfWork uow)
        {
            Db = uow;
        }


        public IQueryable<City> GetCitiesByCountryId(Guid id) => Db.CityRepository.Get().Where(c => c.CountryId == id);

        public IQueryable<City> GetAll() => Db.CityRepository.Get();

        public City Get(Guid id) => Db.CityRepository.Get(id);
        
        
        public async Task<OperationResult> CreateCityAsync(City city)
        {
            var country = Db.CountryRepository.Get(city.CountryId);

            if (country == null)
            {
                return new OperationResult(false, $"Bad country Id: {city.CountryId}", "");
            }

            city.Country = country;

            var result = GetCitiesByCountryId(city.Country.Id)
                .Any(c => string.Equals(c.Name, city.Name, StringComparison.CurrentCultureIgnoreCase));
            if (result)
            {
                return new OperationResult(false, "City is already exist!", "");
            }

            Db.CityRepository.Insert(city);
            await Db.SaveAsync();

            return new OperationResult(true);
        }

        public async Task<OperationResult> EditCityAsync(City city)
        {
            var country = Db.CountryRepository.Get(city.CountryId);
            if (country == null)
            {
                return new OperationResult(false, $"Bad country Id: {city.CountryId}", "");
            }
            city.Country = country;

            var oldCity = Db.CityRepository.Get(city.Id);
            if (oldCity == null)
            {
                return new OperationResult(false, "Not found", "");
            }

            oldCity.Name = city.Name;
            oldCity.CountryId = city.CountryId;
            await Db.SaveAsync();

            return new OperationResult(true);
        }

        public async Task<OperationResult> DeleteCityAsync(Guid id)
        {
            var city = Db.CityRepository.Get(id);
            if (city == null)
            {
                return new OperationResult(false, "Not found", "");
            }

            Db.CityRepository.Delete(city);
            await Db.SaveAsync();
            return new OperationResult(true);
        }
    }
}
