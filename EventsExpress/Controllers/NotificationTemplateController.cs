﻿using System.Collections.Generic;
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
    public class NotificationTemplateController : ControllerBase
    {
        private readonly INotificationTemplateService _notificationTemplateService;
        private readonly IMapper _mapper;

        public NotificationTemplateController(INotificationTemplateService notificationTemplateService, IMapper mapper)
        {
            _notificationTemplateService = notificationTemplateService;
            _mapper = mapper;
        }

        [HttpGet("All")]
        public async Task<ActionResult<IEnumerable<NotificationTemplateDto>>> GetAll()
        {
            return Ok(await _notificationTemplateService.GetAllAsync());
        }

        [HttpGet("{profile}/[action]")]
        public IActionResult GetTemplateProperties(NotificationProfile profile)
        {
            var properties = _notificationTemplateService.GetModelPropertiesByTemplateId(profile);

            return Ok(properties);
        }

        [HttpGet("{id}/Get")]
        public async Task<ActionResult<NotificationTemplateDto>> GetById(NotificationProfile id)
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
            var notificationTemplateDto = _mapper.Map<NotificationTemplateDto>(notificationTemplateViewModel);
            await _notificationTemplateService.UpdateAsync(notificationTemplateDto);

            return Ok();
        }
    }
}
