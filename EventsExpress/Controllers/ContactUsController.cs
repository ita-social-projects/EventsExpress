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
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ContactUsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IContactAdminService _contactAdminService;

        public ContactUsController(
            IMapper mapper,
            IContactAdminService contactAdminService)
        {
            _mapper = mapper;
            _contactAdminService = contactAdminService;
        }

        /// <summary>
        /// This method help to contact users with admins.
        /// </summary>
        /// <param name="model">Param model defines ContactUsViewModel model.</param>
        /// <returns>The method sends message to admin mail.</returns>
        /// <response code="200">Sending is succesfull.</response>
        /// <response code="400">Sending process failed.</response>
        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> ContactAdmins(ContactUsViewModel model)
        {
            var emailTitle = string.Empty;
            if (model.Title == null)
            {
                emailTitle = $"New request from {model.Email} on subject: {model.Subject}";
            }
            else
            {
                emailTitle = model.Title;
            }

            try
            {
                await _contactAdminService.SendMessageToAdmin(new ContactAdminDto
                {
                    Subject = model.Subject,
                    Email = model.Email,
                    MessageText = model.Description,
                    Title = emailTitle,
                });
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// This method have to return all messages for admin.
        /// </summary>
        /// <returns>All messages for admin.</returns>
        /// <response code="200">Return all messages for admin.</response>
        /// <response code="401">If user isn't authorized.</response>
        /// <response code="400">If Return process failed.</response>
        [Authorize]
        [HttpGet("[action]")]
        public IActionResult All()
        {
            return Ok(_mapper.Map<IEnumerable<ContactUsViewModel>>(_contactAdminService.GetAll()));
        }
    }
}
