using System;
using System.Threading.Tasks;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;

        public LocationsController(
            ICountryService countrySrv,
            ICityService citySrv)
        {
            _countryService = countrySrv;
            _cityService = citySrv;
        }

        // Methods for countries CRUD:

        /// <summary>
        /// This method have return all countries.
        /// </summary>
        /// <returns>IEnumerable Countries.</returns>
        /// <response code="200">Return IEnumerable country model.</response>
        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult Countries()
        {
            return Ok(_countryService.GetCountries());
        }

        /// <summary>
        /// This method for edit/create country.
        /// </summary>
        /// <param name="country">Requiered.</param>
        /// <response code="200">Edit/Create country proces success.</response>
        /// <response code="400">If Edit/Create country failed.</response>
        [HttpPost("countries/edit")]
        public async Task<IActionResult> EditCountry(Country country)
        {
            if (ModelState.IsValid)
            {
                var result = country.Id == Guid.Empty
                    ? await _countryService.CreateCountryAsync(country)
                    : await _countryService.EditCountryAsync(country);

                if (result.Successed)
                {
                    return Ok();
                }
            }

            return BadRequest();
        }

        /// <summary>
        /// This method is for delete country.
        /// </summary>
        /// <param name="id">CountryId.</param>
        /// <response code="200">Delete country proces success.</response>
        /// <response code="400">If delete process failed.</response>
        [HttpPost("countries/delete")]
        public async Task<IActionResult> DeleteCountry(Guid id)
        {
            if (id != Guid.Empty)
            {
                var result = await _countryService.DeleteAsync(id);
                if (result.Successed)
                {
                    return Ok();
                }
            }

            return BadRequest();
        }

        // Methods for cities CRUD:

        /// <summary>
        /// This method have return all cities.
        /// </summary>
        /// <returns>IEnumerable cities.</returns>
        /// <response code="200">Return IEnumerable city model.</response>
        [AllowAnonymous]
        [HttpGet("country:{countryId}/[action]")]
        public IActionResult Cities(Guid countryId)
        {
            return Ok(_cityService.GetCitiesByCountryId(countryId));
        }

        /// <summary>
        /// This method for edit/create country.
        /// </summary>
        /// <param name="city">Requiered.</param>
        /// <response code="200">Edit/Create city proces success.</response>
        /// <response code="400">If Edit/Create city failed.</response>
        [HttpPost("cities/edit")]
        public async Task<IActionResult> EditCity(City city)
        {
            if (ModelState.IsValid)
            {
                var result = city.Id == Guid.Empty ?
                    await _cityService.CreateCityAsync(city) :
                    await _cityService.EditCityAsync(city);

                if (result.Successed)
                {
                    return Ok();
                }
            }

            return BadRequest();
        }

        /// <summary>
        /// This method is for delete country.
        /// </summary>
        /// <param name="id">CityId.</param>
        /// <response code="200">Delete city proces success.</response>
        /// <response code="400">If delete process failed.</response>
        [HttpPost("cities/delete")]
        public async Task<IActionResult> DeleteCity(Guid id)
        {
            if (id != Guid.Empty)
            {
                var result = await _cityService.DeleteCityAsync(id);
                if (result.Successed)
                {
                    return Ok();
                }
            }

            return BadRequest();
        }
    }
}
