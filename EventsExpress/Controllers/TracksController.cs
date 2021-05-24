using System.Collections.Generic;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Policies;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize(Policy = PolicyNames.AdminPolicyName)]
    [ApiController]
    public class TracksController : ControllerBase
    {
        private readonly ITrackService _trackService;
        private readonly IMapper _mapper;

        public TracksController(ITrackService trackService, IMapper mapper)
        {
            _trackService = trackService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult All(TrackFilterViewModel filter)
        {
            filter.PageSize = 10;

            var viewModel = new IndexViewModel<TrackDto>
            {
                Items = _mapper.Map<IEnumerable<TrackDto>>(
                    _trackService.GetAllTracks(filter, out int count)),
                PageViewModel = new PageViewModel(count, filter.Page, filter.PageSize),
            };
            return Ok(viewModel);
        }

        [HttpGet]
        public IActionResult GetEntityNames() =>
            Ok(_mapper.Map<IEnumerable<EntityNamesViewModel>>(_trackService.GetDistinctNames()));
    }
}
