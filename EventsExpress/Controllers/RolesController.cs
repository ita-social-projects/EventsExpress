using AutoMapper;
using EventsExpress.Core.IServices;
using EventsExpress.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {

        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RolesController(IRoleService roleService,
                    IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        /// <summary>
        /// This method have to return all roles
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Return IEnumerable RoleDto model</response> 
        [AllowAnonymous]
        [HttpGet]
        public IActionResult All()
        {
            var res = _mapper.Map<IEnumerable<RoleDto>>(_roleService.All());

            return Ok(res);
        }
    }
}