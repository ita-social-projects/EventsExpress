using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<OperationResult> CreateCountryAsync(Country country)
        {
            if (_context.Countries.Any(c => c.Name == country.Name))
            {
                return new OperationResult(false, "Country is already exist", string.Empty);
            }

            Insert(country);
            await _context.SaveChangesAsync();

            return new OperationResult(true);
        }

        public async Task<OperationResult> EditCountryAsync(Country country)
        {
            if (country.Id == Guid.Empty)
            {
                return new OperationResult(false, "Id field is NULL", "Id");
            }

            var oldCountry = _context.Countries.Find(country.Id);
            if (oldCountry == null)
            {
                return new OperationResult(false, "Not found", string.Empty);
            }

            oldCountry.Name = country.Name;
            await _context.SaveChangesAsync();

            return new OperationResult(true);
        }

        public async Task<OperationResult> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return new OperationResult(false, "Id field is NULL", "Id");
            }

            var country = _context.Countries.Find(id);
            if (country == null)
            {
                return new OperationResult(false, "Not found", string.Empty);
            }

            Delete(country);
            await _context.SaveChangesAsync();
            return new OperationResult(true);
        }
    }
}
