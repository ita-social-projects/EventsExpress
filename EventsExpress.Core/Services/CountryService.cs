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
    public class CountryService : BaseService<Country>, ICountryService
    {
        public CountryService(AppDbContext context)
            : base(context)
        {
        }

        public IEnumerable<Country> GetCountries() => _context.Countries;

        public Country GetById(Guid id) => _context.Countries.Find(id);

        public async Task CreateCountryAsync(Country country)
        {
            if (_context.Countries.Any(c => c.Name == country.Name))
            {
                throw new EventsExpressException("Country is already exist");
            }

            Insert(country);
            await _context.SaveChangesAsync();
        }

        public async Task EditCountryAsync(Country country)
        {
            if (country.Id == Guid.Empty)
            {
                throw new EventsExpressException("Id field is NULL");
            }

            var oldCountry = _context.Countries.Find(country.Id);
            if (oldCountry == null)
            {
                throw new EventsExpressException("Not found");
            }

            oldCountry.Name = country.Name;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new EventsExpressException("Id field is NULL");
            }

            var country = _context.Countries.Find(id);
            if (country == null)
            {
                throw new EventsExpressException("Not found");
            }

            Delete(country);
            await _context.SaveChangesAsync();
        }
    }
}
