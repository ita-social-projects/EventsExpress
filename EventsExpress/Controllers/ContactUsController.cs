using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Enums;
using EventsExpress.Filters;
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
        /// <param name="id">Param id defines the message identifier.</param>
        /// <returns>The method sends message to admin mail.</returns>
        /// <response code="200">Sending is succesfull.</response>
        /// <response code="400">Sending process failed.</response>
        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> ContactAdmins(ContactUsViewModel model, Guid id)
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
                    MessageId = id,
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
        /// <response code="400">If Return process failed.</response>
        [Authorize]
        [HttpGet("[action]")]
        public IActionResult All([FromQuery] ContactAdminFilterViewModel filter, Guid id)
        {
            filter.PageSize = 8;
            filter.Page = 1;
            try
            {
                var viewModel = new IndexViewModel<ContactUsViewModel>
                {
                    Items = _mapper.Map<IEnumerable<ContactUsViewModel>>(
                        _contactAdminService.GetAll(filter, id, out int count)),
                    PageViewModel = new PageViewModel(count, filter.Page, filter.PageSize),
                };
                return Ok(viewModel);
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// This method is for change issue status.
        /// </summary>
        /// <param name="issueStatus">Param issueStatus defines the issue status.</param>
        /// <returns>The method returns issue with new status.</returns>
        /// <response code="200">Status changing succesful.</response>
        /// <response code="400">Status changing failed.</response>
        [Authorize]
        [HttpPost("{messageId:Guid}/[action]")]
        public async Task<IActionResult> UpdateStatus(ContactUsViewModel issueStatus)
        {
            await _contactAdminService.SetIssueStatus(issueStatus.MessageId, issueStatus.Status);

            return Ok(issueStatus);
        }

        /// <summary>
        /// This method have to return message.
        /// </summary>
        /// <param name="messageId">Param messageId defines the message identifier.</param>
        /// <returns>The method returns an message by identifier.</returns>
        /// <response code="200">Return MessageInfo model.</response>
        [AllowAnonymous]
        [HttpGet("{messageId:Guid}")]
        public IActionResult GetMessageById(Guid messageId) =>
            Ok(_mapper.Map<ContactUsViewModel>(_contactAdminService.MessageById(messageId)));
    }
}
