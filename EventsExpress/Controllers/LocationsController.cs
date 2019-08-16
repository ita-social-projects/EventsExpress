using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
            ICityService citySrv
            )
        {
            _countryService = countrySrv;
            _cityService = citySrv;
        }


        //Methods for countries CRUD:
        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult Countries()
        {
            return Ok(_countryService.GetCountries());
        }

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


        //Methods for cities CRUD:

        [AllowAnonymous]
        [HttpGet("country:{countryId}/[action]")]
        public IActionResult Cities(Guid countryId)
        {
            return Ok(_cityService.GetCitiesByCountryId(countryId));
        }


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