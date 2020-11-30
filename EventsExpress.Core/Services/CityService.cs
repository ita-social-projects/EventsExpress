using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.BaseService;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;

namespace EventsExpress.Core.Services
{
    public class CityService : BaseService<City>, ICityService
    {
        public CityService(AppDbContext context)
            : base(context)
        {
        }

        public IEnumerable<City> GetCitiesByCountryId(Guid id) =>
            _context.Cities.Where(c => c.CountryId == id);

        public IEnumerable<City> GetAll() => _context.Cities;

        public City GetById(Guid id) => _context.Cities.Find(id);

        public async Task CreateCityAsync(City city)
        {
            var country = _context.Countries
                .Where(x => x.Id == city.CountryId)
                .FirstOrDefault();

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

            Insert(city);
            await _context.SaveChangesAsync();
        }

        public async Task EditCityAsync(City city)
        {
            var country = _context.Countries
                .Where(x => x.Id == city.CountryId)
                .FirstOrDefault();
            if (country == null)
            {
                throw new EventsExpressException($"Bad country Id: {city.CountryId}");
            }

            city.Country = country;

            var oldCity = _context.Cities.Find(city.Id);
            if (oldCity == null)
            {
                throw new EventsExpressException("Not found");
            }

            oldCity.Name = city.Name;
            oldCity.CountryId = city.CountryId;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCityAsync(Guid id)
        {
            var city = _context.Cities.Find(id);
            if (city == null)
            {
                throw new EventsExpressException("Not found");
            }

            Delete(city);
            await _context.SaveChangesAsync();
        }
    }
}
