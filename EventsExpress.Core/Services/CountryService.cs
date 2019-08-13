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
    public class CountryService : ICountryService
    {
        public IUnitOfWork Db { get; set; }

        public CountryService(IUnitOfWork uow)
        {
            Db = uow;
        }


        public IEnumerable<Country> GetCountries() => Db.CountryRepository.Get();

        public Country GetById(Guid id) => Db.CountryRepository.Get(id);


        public async Task<OperationResult> CreateCountryAsync(Country country)
        {
            if (Db.CountryRepository.Get().FirstOrDefault(c => c.Name == country.Name) != null)
            {
                return new OperationResult(false, "Country is already exist", "");
            }
            Db.CountryRepository.Insert(country);
            await Db.SaveAsync();

            return new OperationResult(true);
        }

        public async Task<OperationResult> EditCountryAsync(Country country)
        {
            if (country.Id == null)
            {
                return new OperationResult(false, "Id field is NULL", "Id");
            }

            Country oldCountry = Db.CountryRepository.Get(country.Id);
            if (oldCountry == null)
            {
                return new OperationResult(false, "Not found", "");
            }

            oldCountry.Name = country.Name;

            await Db.SaveAsync();

            return new OperationResult(true);
        }

        public async Task<OperationResult> DeleteAsync(Guid id)
        {
            if (id == null)
            {
                return new OperationResult(false, "Id field is NULL", "Id");
            }
            Country country = Db.CountryRepository.Get(id);
            if (country == null)
            {
                return new OperationResult(false, "Not found", "");
            }

            var result = Db.CountryRepository.Delete(country);
            await Db.SaveAsync();
            return new OperationResult(true);
        }


    }
    
}
