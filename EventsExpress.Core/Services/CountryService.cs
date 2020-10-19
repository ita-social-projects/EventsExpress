using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.Db.IRepo;

namespace EventsExpress.Core.Services
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork db;

        public CountryService(IUnitOfWork uow)
        {
            db = uow;
        }

        public IEnumerable<Country> GetCountries() => db.CountryRepository.Get();

        public Country GetById(Guid id) => db.CountryRepository.Get(id);

        public async Task<OperationResult> CreateCountryAsync(Country country)
        {
            if (db.CountryRepository.Get().Any(c => c.Name == country.Name))
            {
                return new OperationResult(false, "Country is already exist", string.Empty);
            }

            db.CountryRepository.Insert(country);
            await db.SaveAsync();

            return new OperationResult(true);
        }

        public async Task<OperationResult> EditCountryAsync(Country country)
        {
            if (country.Id == Guid.Empty)
            {
                return new OperationResult(false, "Id field is NULL", "Id");
            }

            var oldCountry = db.CountryRepository.Get(country.Id);
            if (oldCountry == null)
            {
                return new OperationResult(false, "Not found", string.Empty);
            }

            oldCountry.Name = country.Name;
            await db.SaveAsync();

            return new OperationResult(true);
        }

        public async Task<OperationResult> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return new OperationResult(false, "Id field is NULL", "Id");
            }

            var country = db.CountryRepository.Get(id);
            if (country == null)
            {
                return new OperationResult(false, "Not found", string.Empty);
            }

            db.CountryRepository.Delete(country);
            await db.SaveAsync();
            return new OperationResult(true);
        }
    }
}
