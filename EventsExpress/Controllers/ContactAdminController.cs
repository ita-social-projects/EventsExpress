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
    public class ContactAdminController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IContactAdminService _contactAdminService;

        public ContactAdminController(
            IMapper mapper,
            IContactAdminService contactAdminService)
        {
            _mapper = mapper;
            _contactAdminService = contactAdminService;
        }

        /// <summary>
        /// This method help to contact users with admins.
        /// </summary>
        /// <param name="model">Param model defines ContactAdminViewModel model.</param>
        /// <returns>The method sends message to admin mail.</returns>
        /// <response code="200">Sending is succesfull.</response>
        /// <response code="400">Sending process failed.</response>
        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<IActionResult> ContactAdmins(ContactAdminViewModel model)
        {
            ContactAdminDto message = _mapper.Map<ContactAdminViewModel, ContactAdminDto>(model);
            var result = await _contactAdminService.SendMessageToAdmin(message);

            return Ok(result);
        }

        /// <summary>
        /// This method have to return all messages for admin.
        /// </summary>
        /// <param name="filter">Param filter provides the ability to filter the list of messages.</param>
        /// <returns>All messages for admin.</returns>
        /// <response code="200">Return all messages for admin.</response>
        /// <response code="400">If Return process failed.</response>
        [Authorize]
        [HttpGet("[action]")]
        public IActionResult All([FromQuery] ContactAdminFilterViewModel filter)
        {
            try
            {
                var viewModel = new IndexViewModel<ContactAdminViewModel>
                {
                    Items = _mapper.Map<IEnumerable<ContactAdminViewModel>>(
                        _contactAdminService.GetAll(filter, out int count)),
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
        public async Task<IActionResult> UpdateStatus(UpdateIssueStatusViewModel issueStatus)
        {
            await _contactAdminService.UpdateIssueStatus(issueStatus.MessageId, issueStatus.ResolutionDetails, issueStatus.Status);

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
            Ok(_mapper.Map<ContactAdminViewModel>(_contactAdminService.MessageById(messageId)));
    }
}
