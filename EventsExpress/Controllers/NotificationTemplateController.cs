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
        public async Task<ActionResult> Add(NotificationTemplateDTO notificationTemplateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _notificationTemplateService.AddAsync(notificationTemplateDto);
            return Ok();
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<NotificationTemplateDTO>>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            return Ok(await _notificationTemplateService.GetAsync(page, pageSize));
        }

        [HttpGet("{id:Guid}/Get")]
        public async Task<ActionResult<NotificationTemplateDTO>> GetById(Guid id)
        {
            var notificationTemplate = await _notificationTemplateService.GetByIdAsync(id);

            if (notificationTemplate == null)
            {
                return NotFound(id);
            }

            return Ok(notificationTemplate);
        }

        [HttpPost("{id:Guid}/Edit")]
        public async Task<ActionResult> Update(Guid id, NotificationTemplateDTO notificationTemplateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _notificationTemplateService.UpdateAsync(notificationTemplateDto);
            return Ok(notificationTemplateDto);
        }
    }
}
