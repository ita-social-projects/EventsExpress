using EventsExpress.Core.Infrastructure;
using EventsExpress.Db.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventsExpress.Core.IServices
{
    public interface ICountryService
    {
        IEnumerable<Country> GetCountries();
        //Country GetByName(string name);
        Country GetById(Guid id);
        Task<OperationResult> CreateCountryAsync(Country country);
        Task<OperationResult> EditCountryAsync(Country country);
        Task<OperationResult> DeleteAsync(Guid id);
    }
}
