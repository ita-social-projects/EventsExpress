using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
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

        [HttpPost("action")]
        public async Task<IActionResult> Create([FromBody] UnitOfMeasuringDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _unitOfMeasuringService.Create(_mapper.Map<UnitOfMeasuringDto, UnitOfMeasuringDTO>(model));
            if (result.Successed)
            {
                return Ok(result.Property);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("action")]
        public async Task<IActionResult> Edit([FromBody] UnitOfMeasuringDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _unitOfMeasuringService.Edit(_mapper.Map<UnitOfMeasuringDto, UnitOfMeasuringDTO>(model));
            if (result.Successed)
            {
                return Ok(result.Property);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("action")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _unitOfMeasuringService.Delete(id);
            if (result.Successed)
            {
                return Ok(result.Property);
            }

            return BadRequest(result.Message);
        }


        [HttpGet("action")]
        public IActionResult GetAll()
        {
            return Ok(_mapper.Map<ICollection<UnitOfMeasuringDTO>, ICollection<UnitOfMeasuringDto>>(_unitOfMeasuringService.GetAll()));
        }

        [HttpGet("action")]
        public IActionResult GetById(Guid id)
        {
            return Ok(_mapper.Map<UnitOfMeasuringDTO, UnitOfMeasuringDto>(_unitOfMeasuringService.GetById(id)));
        }
    }
}
