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
        private readonly AppDbContext _context;

        public CountryService(AppDbContext context)
            : base(context)
        {
            _context = context;
        }

        public IEnumerable<Country> GetCountries() => Get();

        public Country GetById(Guid id) => Get(id);

        public async Task<OperationResult> CreateCountryAsync(Country country)
        {
            if (Get().Any(c => c.Name == country.Name))
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

            var oldCountry = Get(country.Id);
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

            var country = Get(id);
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
