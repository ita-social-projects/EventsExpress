using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Enums;
using EventsExpress.ViewModels;
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
        private readonly IMapper _mapper;

        public NotificationTemplateController(INotificationTemplateService notificationTemplateService, IMapper mapper)
        {
            _notificationTemplateService = notificationTemplateService;
            _mapper = mapper;
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<NotificationTemplateDTO>>> GetAll()
        {
            return Ok(await _notificationTemplateService.GetAllAsync());
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
        public async Task<ActionResult> Update(EditNotificationTemplateViewModel notificationTemplateViewModel)
        {
            var notificationTemplateDto = _mapper.Map<NotificationTemplateDTO>(notificationTemplateViewModel);
            await _notificationTemplateService.UpdateAsync(notificationTemplateDto);

            return Ok();
        }
    }
}
