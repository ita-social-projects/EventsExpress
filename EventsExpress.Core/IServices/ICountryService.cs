using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Db.Entities;

namespace EventsExpress.Core.IServices
{
    public interface ICountryService
    {
        IEnumerable<Country> GetCountries();

        Country GetById(Guid id);

        Task<OperationResult> CreateCountryAsync(Country country);

        Task<OperationResult> EditCountryAsync(Country country);

        Task<OperationResult> DeleteAsync(Guid id);
    }
}
