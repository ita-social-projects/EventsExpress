using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.DTO;
using EventsExpress.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class InventoryController : Controller
    {
        private readonly IInventoryService _inventoryService;
        private readonly IMapper _mapper;

        public InventoryController(
            IInventoryService inventoryService,
            IMapper mapper)
        {
            _inventoryService = inventoryService;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddInventar([FromBody] InventoryDto model, Guid eventId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _inventoryService.AddInventar(eventId, _mapper.Map<InventoryDto, InventoryDTO>(model));
            if (result.Successed)
            {
                return Ok(result.Property);
            }

            return BadRequest(result.Message);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> EditInventar([FromBody] InventoryDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _inventoryService.EditInventar(_mapper.Map<InventoryDto, InventoryDTO>(model));
            return Ok(result.Property);
        }

        [HttpGet("[action]")]
        public IActionResult GetInventar(Guid eventId)
        {
            return Ok(_mapper.Map<ICollection<InventoryDTO>, ICollection<InventoryDto>>(_inventoryService.GetInventar(eventId).ToList()));
        }

        [HttpGet("[action]")]
        public IActionResult GetInventarById(Guid inventoryId)
        {
            return Ok(_mapper.Map<InventoryDTO, InventoryDto>(_inventoryService.GetInventarById(inventoryId)));
        }
    }
}
