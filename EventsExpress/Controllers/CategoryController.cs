using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Filters;
using EventsExpress.ModelBinders;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Policy = "AdminPolicy")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(
            ICategoryService categoryService,
            IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        /// <summary>
        /// This method have to return all categories.
        /// </summary>
        /// <returns>The method returns all categories.</returns>
        /// <response code="200">Return IEnumerable CategoryDto model.</response>
        [HttpGet("[action]")]
        [AllowAnonymous]
        public IActionResult All() =>
            Ok(_mapper.Map<IEnumerable<CategoryViewModel>>(_categoryService.GetAllCategories()));

        /// <summary>
        /// This method is for create categories.
        /// </summary>
        /// <param name="model">Param model defines CategoryCreateViewModel model.</param>
        /// <returns>The method returns created category.</returns>
        /// <response code="200">Create category proces success.</response>
        /// <response code="400">If Create process failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> Create([ModelBinder(typeof(TrimModelBinder))] CategoryCreateViewModel model)
        {
            await _categoryService.Create(model.Name);
            return Ok();
        }

        /// <summary>
        /// This method is for edit categories.
        /// </summary>
        /// <param name="model">Param model defines CategoryEditViewModel model.</param>
        /// <returns>The method returns edited category.</returns>
        /// <response code="200">Edit category proces success.</response>
        /// <response code="400">If Edit process failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> Edit([ModelBinder(typeof(TrimModelBinder))] CategoryEditViewModel model)
        {
            await _categoryService.Edit(_mapper.Map<CategoryEditViewModel, CategoryDto>(model));
            return Ok();
        }

        /// <summary>
        /// This method is for delete category.
        /// </summary>
        /// <param name="id">Param id defines category identifier.</param>
        /// <returns>The method returns deleted category.</returns>
        /// <response code="200">Delete category proces success.</response>
        /// <response code="400">If delete process failed.</response>
        [HttpPost("[action]/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest();
            }

            await _categoryService.Delete(id);

            return Ok();
        }
    }
}
