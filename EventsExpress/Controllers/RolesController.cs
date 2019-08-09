using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class RolesController : ControllerBase
    {

        private IRoleService _roleService;
        private IMapper _mapper;

        public RolesController(IRoleService roleService,
                    IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }


        [AllowAnonymous]
        [HttpGet]
        public IActionResult All()
        {
            var res = _mapper.Map<IEnumerable<Role>, IEnumerable<RoleDto>>(_roleService.All());

            return Ok(res);
        }
    }
}