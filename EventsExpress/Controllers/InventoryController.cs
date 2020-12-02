using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Filters;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
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

        /// <summary>
        /// This method have to add inventar to event..
        /// </summary>
        /// <param name="model">Required.</param>
        /// <param name="eventId">Required.</param>
        /// <response code="200">Adding inventar from event proces success.</response>
        /// <response code="400">If adding inventar from event process failed.</response>
        [HttpPost("[action]")]
        [UserAccessTypeFilter]
        public async Task<IActionResult> AddInventar(Guid eventId, [FromBody] InventoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _inventoryService.AddInventar(eventId, _mapper.Map<InventoryViewModel, InventoryDTO>(model));
            if (result.Successed)
            {
                return Ok(result.Property);
            }

            return BadRequest(result.Message);
        }

        /// <summary>
        /// This method is for edit inventar.
        /// </summary>
        /// <param name="model">Required.</param>
        /// <response code="200">Edit inventar proces success.</response>
        /// <response code="400">If Edit process failed.</response>
        [HttpPost("[action]")]
        [UserAccessTypeFilter]
        public async Task<IActionResult> EditInventar(Guid eventId, [FromBody] InventoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _inventoryService.EditInventar(_mapper.Map<InventoryViewModel, InventoryDTO>(model));
            return Ok(result.Property);
        }

        /// <summary>
        /// This method is for delete inventar.
        /// </summary>
        /// <param name="itemId)">Required.</param>
        /// <response code="200">Delete inventar proces success.</response>
        /// <response code="400">If id param is empty.</response>
        [HttpPost("[action]")]
        [UserAccessTypeFilter]
        public async Task<IActionResult> DeleteInventar(Guid eventId, Guid itemId)
        {
            if (itemId == Guid.Empty)
            {
                return BadRequest("id is empty");
            }

            var result = await _inventoryService.DeleteInventar(itemId);
            return Ok(result.Property);
        }

        /// <summary>
        /// This method have to return all inventories from event.
        /// </summary>
        /// <param name="eventId">Required.</param>
        /// <returns>All inventories from event.</returns>
        /// <response code="200">Return IEnumerable InventoryDto.</response>
        [HttpGet("[action]")]
        public IActionResult GetInventar(Guid eventId)
        {
            if (eventId == Guid.Empty)
            {
                return BadRequest("Event id is empty");
            }
            else
            {
                return Ok(_mapper.Map<ICollection<InventoryDTO>, ICollection<InventoryViewModel>>(_inventoryService.GetInventar(eventId).ToList()));
            }
        }

        /// <summary>
        /// This method have to return inventar.
        /// </summary>
        /// <param name="inventoryId">Required.</param>
        /// <returns>Inventory.</returns>
        /// <response code="200">Return InventoryDto model.</response>
        [HttpGet("[action]")]
        public IActionResult GetInventarById(Guid inventoryId)
        {
            if (inventoryId == Guid.Empty)
            {
                return BadRequest("Inventory id is empty");
            }
            else
            {
                return Ok(_mapper.Map<InventoryDTO, InventoryViewModel>(_inventoryService.GetInventarById(inventoryId)));
            }
        }
    }
}
