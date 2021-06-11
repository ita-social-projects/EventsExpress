using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Policies;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitOfMeasuringController : Controller
    {
        private readonly IUnitOfMeasuringService _unitOfMeasuringService;
        private readonly IMapper _mapper;

        public UnitOfMeasuringController(
            IUnitOfMeasuringService unitOfMeasuringService,
            IMapper mapper)
        {
            _unitOfMeasuringService = unitOfMeasuringService;
            _mapper = mapper;
        }

        /// <summary>
        /// This method is for create unit of measuring.
        /// </summary>
        /// <param name="model">Param model defines UnitOfMeasuringViewModel model.</param>
        /// <returns>The method returns created unit of meashuring.</returns>
        /// <response code="200">Create unit of measuring proces success.</response>
        /// <response code="401">If user isn't authorized.</response>
        /// <response code="403">If user's role isn't admin.</response>
        /// <response code="400">If Create process failed.</response>
        [Authorize(Policy = PolicyNames.AdminPolicyName)]
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] UnitOfMeasuringCreateViewModel model)
        {
                UnitOfMeasuringDto dTO = _mapper.Map<UnitOfMeasuringCreateViewModel, UnitOfMeasuringDto>(model);
                var result = await _unitOfMeasuringService.Create(dTO);

                return Ok(result);
        }

        /// <summary>
        /// This method is for edit unit of measuring.
        /// </summary>
        /// <param name="model">Param model defines UnitOfMeasuringViewModel model.</param>
        /// <returns>The method returns edited unit of meashuring.</returns>
        /// <response code="200">Edit unit of measuring proces success.</response>
        /// <response code="401">If user isn't authorized.</response>
        /// <response code="403">If user's role isn't admin.</response>
        /// <response code="400">If Edit process failed.</response>
        [Authorize(Policy = PolicyNames.AdminPolicyName)]
        [HttpPost("[action]")]
        public async Task<IActionResult> Edit([FromBody] UnitOfMeasuringCreateViewModel model)
        {
            if (model == null)
            {
                throw new EventsExpressException("Null object");
            }

            var result = await _unitOfMeasuringService.Edit(_mapper.Map<UnitOfMeasuringCreateViewModel, UnitOfMeasuringDto>(model));

            return Ok(result);
        }

        /// <summary>
        /// This method have to return all units of measuring.
        /// </summary>
        /// <returns>All units of measuring.</returns>
        /// <response code="200">Return all units of measuring.</response>
        /// <response code="401">If user isn't authorized.</response>
        /// <response code="400">If Return process failed.</response>
        [Authorize]
        [HttpGet("[action]")]
        public IActionResult All()
        {
            return Ok(_mapper.Map<IEnumerable<UnitOfMeasuringDto>, IEnumerable<UnitOfMeasuringViewModel>>(_unitOfMeasuringService.GetAll()));
        }

        /// <summary>
        /// This method have to return unit of measuring by id.
        /// </summary>
        /// <param name="id">Param id defines the unit of measuring identifier.</param>
        /// <returns>The method returns unit of measuring by identifier.</returns>
        /// <response code="200">Return Unit of measuring by id.</response>
        /// <response code="401">If user isn't authorized.</response>
        /// <response code="403">If user's role isn't admin.</response>
        /// <response code="400">If Return process  failed.</response>
        [Authorize(Policy = PolicyNames.AdminPolicyName)]
        [HttpGet("[action]")]
        public IActionResult GetById(Guid id)
        {
            var item = _unitOfMeasuringService.GetById(id);
            return Ok(_mapper.Map<UnitOfMeasuringDto, UnitOfMeasuringViewModel>(item));
        }

        /// <summary>
        /// This method is for delete unit of measuring.
        /// </summary>
        /// <param name="id">Param id defines the unit of measuring identifier.</param>
        /// <returns>The method returns deleted unit of meashuring.</returns>
        /// <response code="200">Delete unit of measuring proces success.</response>
        /// <response code="401">If user isn't authorized.</response>
        /// <response code="403">If user's role isn't admin.</response>
        /// <response code="400">If delete process failed.</response>
        [Authorize(Policy = PolicyNames.AdminPolicyName)]
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _unitOfMeasuringService.Delete(id);

            return Ok();
        }
    }
}
