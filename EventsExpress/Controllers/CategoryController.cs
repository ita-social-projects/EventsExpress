using AutoMapper;
using EventsExpress.Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return Ok(_categoryService.GetAllCategories());
        }


    }
 }