using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsExpress.Core.Exceptions;
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

        public async Task CreateCountryAsync(Country country)
        {
            if (_db.CountryRepository.Get().Any(c => c.Name == country.Name))
            {
                throw new EventsExpressException("Country is already exist");
            }

            _db.CountryRepository.Insert(country);
            await _db.SaveAsync();
        }

        public async Task EditCountryAsync(Country country)
        {
            if (country.Id == Guid.Empty)
            {
                throw new EventsExpressException("Id field is NULL");
            }

            var oldCountry = _db.CountryRepository.Get(country.Id);
            if (oldCountry == null)
            {
                throw new EventsExpressException("Not found");
            }

            oldCountry.Name = country.Name;
            await _db.SaveAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new EventsExpressException("Id field is NULL");
            }

            var country = _db.CountryRepository.Get(id);
            if (country == null)
            {
                throw new EventsExpressException("Not found");
            }

            _db.CountryRepository.Delete(country);
            await _db.SaveAsync();
        }
    }
}
