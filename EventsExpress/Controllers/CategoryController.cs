using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
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
        /// <returns>CategoryDto model.</returns>
        /// <response code="200">Return IEnumerable CategoryDto model.</response>
        [HttpGet("[action]")]
        [AllowAnonymous]
        public IActionResult All() =>
            Ok(_mapper.Map<IEnumerable<CategoryDto>>(_categoryService.GetAllCategories()));

        /// <summary>
        /// This method is for edit and create categories.
        /// </summary>
        /// <param name="model">Required.</param>
        /// <response code="200">Edit/Create category proces success.</response>
        /// <response code="400">If Edit/Create process failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> Edit(CategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.Id == Guid.Empty)
            {
                await _categoryService.Create(model.Name);
            }
            else
            {
                await _categoryService.Edit(_mapper.Map<CategoryDto, CategoryDTO>(model));
            }

            return Ok();
        }

        /// <summary>
        /// This method is for delete category.
        /// </summary>
        /// <param name="id">Required.</param>
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
