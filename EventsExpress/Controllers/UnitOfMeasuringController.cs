using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
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
        /// <param name="model">Required.</param>
        /// <response code="200">Create unit of measuring proces success.</response>
        /// <response code="401">If user isn't authorized.</response>
        /// <response code="403">If user's role isn't admin.</response>
        /// <response code="400">If Create process failed.</response>

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] UnitOfMeasuringViewModel model)
        {
            var result = await _unitOfMeasuringService.Create(_mapper.Map<UnitOfMeasuringViewModel, UnitOfMeasuringDTO>(model));

            return Ok(result);
        }

        /// <summary>
        /// This method is for edit unit of measuring.
        /// </summary>
        /// <param name="model">Required.</param>
        /// <response code="200">Edit unit of measuring proces success.</response>
        /// <response code="401">If user isn't authorized.</response>
        /// <response code="403">If user's role isn't admin.</response>
        /// <response code="400">If Edit process failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> Edit([FromBody] UnitOfMeasuringViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _unitOfMeasuringService.Edit(_mapper.Map<UnitOfMeasuringViewModel, UnitOfMeasuringDTO>(model));

            return Ok(result);
        }

        /// <summary>
        /// This method have to return all units of measuring.
        /// </summary>
        /// <returns>All units of measuring.</returns>
        /// <response code="200">Return all units of measuring.</response>
        /// <response code="401">If user isn't authorized.</response>
        /// <response code="403">If user's role isn't admin.</response>
        /// <response code="400">If Return process failed.</response>
        [HttpGet("[action]")]
        public IActionResult All()
        {
            return Ok(_mapper.Map<IEnumerable<UnitOfMeasuringDTO>, IEnumerable<UnitOfMeasuringViewModel>>(_unitOfMeasuringService.GetAll()));
        }

        /// <summary>
        /// This method have to return unit of measuring by id.
        /// </summary>
        /// <param name="id">Required.</param>
        /// <returns>Unit of measuring.</returns>
        /// <response code="200">Return Unit of measuring by id.</response>
        /// <response code="401">If user isn't authorized.</response>
        /// <response code="403">If user's role isn't admin.</response>
        /// <response code="400">If Return process  failed.</response>
        [HttpGet("[action]")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_mapper.Map<UnitOfMeasuringDTO, UnitOfMeasuringViewModel>(_unitOfMeasuringService.GetById(id)));
        }

        /// <summary>
        /// This method is for delete unit of measuring.
        /// </summary>
        /// <param name="id">Required.</param>
        /// <response code="200">Delete unit of measuring proces success.</response>
        /// <response code="401">If user isn't authorized.</response>
        /// <response code="403">If user's role isn't admin.</response>
        /// <response code="400">If delete process failed.</response>
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            await _unitOfMeasuringService.Delete(id);

            return Ok();
        }
    }
}
