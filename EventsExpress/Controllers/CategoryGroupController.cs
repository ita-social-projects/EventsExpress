using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.ModelBinders;
using EventsExpress.Policies;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = PolicyNames.AdminPolicyName)]
    [ApiController]
    public class CategoryGroupController : Controller
    {
        private readonly ICategoryGroupService _categoryGroupService;
        private readonly IMapper _mapper;

        public CategoryGroupController(
            ICategoryGroupService categoryGroupService,
            IMapper mapper)
        {
            _categoryGroupService = categoryGroupService;
            _mapper = mapper;
        }

        /// <summary>
        /// This method have to return all category groups.
        /// </summary>
        /// <returns>The method returns all category groups.</returns>
        /// <response code="200">Returns IEnumerable CategoryGroupDto model.</response>
        [HttpGet("[action]")]
        [AllowAnonymous]
        public IActionResult All() =>
            Ok(_mapper.Map<IEnumerable<CategoryGroupViewModel>>(_categoryGroupService.GetAllGroups()));

        /// <summary>
        /// This method have to return specified by ID category group.
        /// </summary>
        /// <param name="id">Param id defined category group identifier.</param>
        /// <returns>The method returns specified by ID category group.</returns>
        /// <response code="200">Returns CategoryGroupDto model.</response>
        [HttpGet("[action]/{id}")]
        [AllowAnonymous]
        public IActionResult Get(Guid id) =>
            Ok(_mapper.Map<CategoryGroupViewModel>(_categoryGroupService.GetById(id)));
    }
}
