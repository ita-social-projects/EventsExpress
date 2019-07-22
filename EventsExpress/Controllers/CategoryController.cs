using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private ICategoryService _categoryService;
        private IMapper _mapper;

        public CategoryController(ICategoryService categoryService,
                    IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }


        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult All()
        {
            var res = _mapper.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryDto>>(_categoryService.GetAllCategories());

            return Ok(res);
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public IActionResult Delete(Guid id)
        {
            var res = _categoryService.Delete(id);

            return Ok(res);
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Edit(CategoryDto model)
        {

            var res = model.Id == Guid.Empty ? await _categoryService.Create(model.Name)
                                       : await _categoryService.Edit(_mapper.Map<CategoryDto, CategoryDTO>(model));
            if (res.Successed)
            {
                return Ok();
            }
            return BadRequest(res.Message);
        }

    }
}