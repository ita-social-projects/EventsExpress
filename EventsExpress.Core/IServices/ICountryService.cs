using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Db.Entities;

namespace EventsExpress.Core.IServices
{
    public interface ICountryService
    {
        IEnumerable<Country> GetCountries();

        Country GetById(Guid id);

        Task CreateCountryAsync(Country country);

        Task EditCountryAsync(Country country);

        Task DeleteAsync(Guid id);
    }
}
