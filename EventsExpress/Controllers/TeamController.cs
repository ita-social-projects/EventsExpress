using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.Entities;
using EventsExpress.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;
        private readonly IMapper _mapper;

        public TeamController(
            ITeamService teamService,
            IMapper mapper)
        {
            _teamService = teamService;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        public IActionResult All()
        {
            try
            {
                return Ok(_mapper.Map<IEnumerable<TeamDto>>(_teamService.All()));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromForm]TeamCreateDto model)
        {
            var result = await _teamService.AddTeamAsync(model);

            if (result.Successed)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddDev([FromForm] DeveloperCreateDto model)
        {
            var result = await _teamService.AddDevAsync(model);

            if (result.Successed)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
