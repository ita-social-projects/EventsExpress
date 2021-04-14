using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationTemplateController : Controller
    {
        private readonly INotificationTemplateService _notificationTemplateService;

        public NotificationTemplateController(INotificationTemplateService notificationTemplateService)
        {
            _notificationTemplateService = notificationTemplateService;
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<NotificationTemplateDTO>>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            return Ok(await _notificationTemplateService.GetAsync(page, pageSize));
        }

        [HttpGet("{id:int}/Get")]
        public async Task<ActionResult<NotificationTemplateDTO>> GetById(NotificationProfile id)
        {
            var notificationTemplate = await _notificationTemplateService.GetByIdAsync(id);

            if (notificationTemplate == null)
            {
                return NotFound(id);
            }

            return Ok(notificationTemplate);
        }

        [HttpPost("Edit")]
        public async Task<ActionResult> Update(NotificationTemplateDTO notificationTemplateDto)
        {
            await _notificationTemplateService.UpdateAsync(notificationTemplateDto);
            return Ok(notificationTemplateDto);
        }
    }
}
