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
        private readonly IUnitOfWork _db;

        public CountryService(IUnitOfWork uow)
        {
            _db = uow;
        }

        public IEnumerable<Country> GetCountries() => _db.CountryRepository.Get();

        public Country GetById(Guid id) => _db.CountryRepository.Get(id);

        public async Task<OperationResult> CreateCountryAsync(Country country)
        {
            if (_db.CountryRepository.Get().Any(c => c.Name == country.Name))
            {
                return new OperationResult(false, "Country is already exist", string.Empty);
            }

            _db.CountryRepository.Insert(country);
            await _db.SaveAsync();

            return new OperationResult(true);
        }

        public async Task<OperationResult> EditCountryAsync(Country country)
        {
            if (country.Id == Guid.Empty)
            {
                return new OperationResult(false, "Id field is NULL", "Id");
            }

            var oldCountry = _db.CountryRepository.Get(country.Id);
            if (oldCountry == null)
            {
                return new OperationResult(false, "Not found", string.Empty);
            }

            oldCountry.Name = country.Name;
            await _db.SaveAsync();

            return new OperationResult(true);
        }

        public async Task<OperationResult> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return new OperationResult(false, "Id field is NULL", "Id");
            }

            var country = _db.CountryRepository.Get(id);
            if (country == null)
            {
                return new OperationResult(false, "Not found", string.Empty);
            }

            _db.CountryRepository.Delete(country);
            await _db.SaveAsync();
            return new OperationResult(true);
        }
    }
}
