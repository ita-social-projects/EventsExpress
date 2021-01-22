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
        /// <param name="eventId">Param eventId defines the event identifier.</param>
        /// <param name="model">Param model provides access to inventory properties.</param>
        /// <returns>The method returns created inventory.</returns>
        /// <response code="200">Adding inventar from event proces success.</response>
        /// <response code="400">If adding inventar from event process failed.</response>
        [HttpPost("{eventId:Guid}/[action]")]
        [UserAccessTypeFilter]
        public async Task<IActionResult> AddInventar(Guid eventId, [FromBody] InventoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _inventoryService.AddInventar(eventId, _mapper.Map<InventoryViewModel, InventoryDTO>(model));

            return Ok(result);
        }

        /// <summary>
        /// This method is for edit inventar.
        /// </summary>
        /// <param name="eventId">Param eventId defines the event identifier.</param>
        /// <param name="model">Param model provides access to inventory properties.</param>
        /// <returns>The method returns edited inventory.</returns>
        /// <response code="200">Edit inventar proces success.</response>
        /// <response code="400">If Edit process failed.</response>
        [HttpPost("{eventId:Guid}/[action]")]
        [UserAccessTypeFilter]
        public async Task<IActionResult> EditInventar(Guid eventId, [FromBody] InventoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _inventoryService.EditInventar(_mapper.Map<InventoryViewModel, InventoryDTO>(model));

            return Ok(result);
        }

        /// <summary>
        /// This method is for delete inventar.
        /// </summary>
        /// <param name="eventId">Param eventId defines the event identifier.</param>
        /// <param name="itemId">Param itemId defines the item identifier.</param>
        /// <returns>The method returns deleted inventory.</returns>
        /// <response code="200">Delete inventar proces success.</response>
        /// <response code="400">If id param is empty.</response>
        [HttpPost("{eventId:Guid}/[action]")]
        [UserAccessTypeFilter]
        public async Task<IActionResult> DeleteInventar(Guid eventId, Guid itemId)
        {
            if (itemId == Guid.Empty)
            {
                return BadRequest("id is empty");
            }

            var result = await _inventoryService.DeleteInventar(itemId);

            return Ok(result);
        }

        /// <summary>
        /// This method have to return all inventories from event.
        /// </summary>
        /// <param name="eventId">Param eventId defines the event identifier.</param>
        /// <returns>The method returns inventory by event identifier.</returns>
        /// <response code="200">Return IEnumerable InventoryDto.</response>
        [HttpGet("{eventId:Guid}/[action]")]
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
        /// <param name="inventoryId">Param inventoryId defines the inventory identifier.</param>
        /// <returns>The method returns inventory by identifier.</returns>
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
