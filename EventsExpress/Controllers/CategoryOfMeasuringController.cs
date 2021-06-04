using System;
using System.Collections.Generic;
using AutoMapper;
using EventsExpress.Core.IServices;
using EventsExpress.Policies;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = PolicyNames.AdminPolicyName)]
    [ApiController]
    public class CategoryOfMeasuringController : ControllerBase
    {
        private readonly ICategoryOfMeasuringService _categoryOfMeasuringService;
        private readonly IMapper _mapper;

        public CategoryOfMeasuringController(
            ICategoryOfMeasuringService categoryOfMeasuringService,
            IMapper mapper)
        {
            _categoryOfMeasuringService = categoryOfMeasuringService;
            _mapper = mapper;
        }

        /// <summary>
        /// This method have to return all categoriesOfMeasuring.
        /// </summary>
        /// <returns>The method returns all categoriesOfMeasuring.</returns>
        /// <response code="200">Return IEnumerable CategoryOfMeasuringDto model.</response>
        [HttpGet("[action]")]
        public IActionResult GetAll() =>
            Ok(_mapper.Map<IEnumerable<CategoryOfMeasuringViewModel>>(_categoryOfMeasuringService.GetAllCategories()));

        /// <summary>
        /// This method have to return category.
        /// </summary>
        /// <param name="id">Param id defines the category identifier.</param>
        /// <returns>The method returns an category by identifier.</returns>
        /// <response code="200">Return CategoryInfo model.</response>
        [AllowAnonymous]
        [HttpGet("{id:Guid}")]
        public IActionResult GetById(Guid id) =>
            Ok(_mapper.Map<ContactAdminViewModel>(_categoryOfMeasuringService.GetCategoryOfMeasuringById(id)));
    }
}
