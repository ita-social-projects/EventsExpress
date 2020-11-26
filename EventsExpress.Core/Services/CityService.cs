using System;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.BaseService;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;

namespace EventsExpress.Core.Services
{
    public class CityService : BaseService<City>, ICityService
    {
        private readonly AppDbContext _context;

        public CityService(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public IQueryable<City> GetCitiesByCountryId(Guid id) => 
            Get().Where(c => c.CountryId == id);

        public IQueryable<City> GetAll() => Get();

        public City GetById(Guid id) => Get(id);

        public async Task<OperationResult> CreateCityAsync(City city)
        {
            var country = _context.Countries
                .Where(x => x.Id == city.CountryId)
                .FirstOrDefault();

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

            Insert(city);
            await _context.SaveChangesAsync();

            return new OperationResult(true);
        }

        public async Task<OperationResult> EditCityAsync(City city)
        {
            var country = _context.Countries
                .Where(x => x.Id == city.CountryId)
                .FirstOrDefault();
            if (country == null)
            {
                return new OperationResult(false, $"Bad country Id: {city.CountryId}", string.Empty);
            }

            city.Country = country;

            var oldCity = Get(city.Id);
            if (oldCity == null)
            {
                return new OperationResult(false, "Not found", string.Empty);
            }

            oldCity.Name = city.Name;
            oldCity.CountryId = city.CountryId;
            await _context.SaveChangesAsync();

            return new OperationResult(true);
        }

        public async Task<OperationResult> DeleteCityAsync(Guid id)
        {
            var city = Get(id);
            if (city == null)
            {
                return new OperationResult(false, "Not found", string.Empty);
            }

            Delete(city);
            await _context.SaveChangesAsync();
            return new OperationResult(true);
        }
    }
}
