using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsExpress.Core.Services
{
    public class CityService : ICityService
    {
        public IUnitOfWork Db { get; set; }

        public CityService(IUnitOfWork uow)
        {
            Db = uow;
        }


        public IQueryable<City> GetCitiesByCountryId(Guid id) => Db.CityRepository.Filter(filter: c => c.CountryId == id);

        public IQueryable<City> GetAll() => Db.CityRepository.Get();

        public City Get(Guid id) => Db.CityRepository.Get(id);
        
        
        public async Task<OperationResult> CreateCityAsync(City city)
        {
            var counry = Db.CountryRepository.Get(city.CountryId);

            if (counry == null)
            {
                return new OperationResult(false, $"Bad country Id: {city.CountryId}", "");
            }

            city.Country = counry;

            var result = GetCitiesByCountryId(city.Country.Id)
                .Where(c => c.Name.ToUpper() == city.Name.ToUpper())
                .FirstOrDefault();

            if (result != null)
            {
                return new OperationResult(false, "City is already exist!", "");
            }

            Db.CityRepository.Insert(city);
            await Db.SaveAsync();

            return new OperationResult(true);
        }

        public async Task<OperationResult> EditCityAsync(City city)
        {
            if (city.Id == null)
            {
                return new OperationResult(false, "Id field is NULL", "Id");
            }
            // Build new entity
            var counry = Db.CountryRepository.Get(city.CountryId);
            if (counry == null)
            {
                return new OperationResult(false, $"Bad country Id: {city.CountryId}", "");
            }
            city.Country = counry;

            // Find old entity
            City oldCity = Db.CityRepository.Get(city.Id);
            if (oldCity == null)
            {
                return new OperationResult(false, "Not found", "");
            }

            // Update
            oldCity.Name = city.Name;
            oldCity.CountryId = city.CountryId;

            await Db.SaveAsync();

            return new OperationResult(true);
        }

        public async Task<OperationResult> DeleteCityAsync(Guid id)
        {
            if (id == null)
            {
                return new OperationResult(false, "Id field is NULL", "");
            }
            City city = Db.CityRepository.Get(id);
            if (city == null)
            {
                return new OperationResult(false, "Not found", "");
            }

            var result = Db.CityRepository.Delete(city);
            await Db.SaveAsync();
            return new OperationResult(true);
        }
    }
}
