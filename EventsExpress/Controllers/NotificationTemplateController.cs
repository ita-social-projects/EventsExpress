using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationTemplateController : Controller
    {
        private readonly INotificationTemplateService _notificationTemplateService;

        public NotificationTemplateController(INotificationTemplateService notificationTemplateService)
        {
            _notificationTemplateService = notificationTemplateService;
        }

        [HttpPost("Add")]
        public async Task<ActionResult> Add(NotificationTemplateDto notificationTemplateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _notificationTemplateService.AddAsync(notificationTemplateDto);
            return Ok();
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<NotificationTemplateDto>>> GetAll()
        {
            return Ok(await _notificationTemplateService.GetAllAsync());
        }

        [HttpGet("{id:Guid}/Get")]
        public async Task<ActionResult<NotificationTemplateDto>> GetById(Guid id)
        {
            var notificationTemplate = await _notificationTemplateService.GetByIdAsync(id);

            if (notificationTemplate == null)
            {
                return NotFound(id);
            }

            return Ok(notificationTemplate);
        }

        [HttpPost("{id:Guid}/Edit")]
        public async Task<ActionResult> Update(Guid id, NotificationTemplateDto notificationTemplateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _notificationTemplateService.UpdateAsync(notificationTemplateDto);
            return Ok();
        }

        [HttpPost("{id:Guid}/[action]")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var notificationTemplate = await _notificationTemplateService.GetByIdAsync(id);

            if (notificationTemplate == null)
            {
                return NotFound();
            }

            await _notificationTemplateService.DeleteByIdAsync(notificationTemplate.Id);

            return Ok();
        }
    }
}
