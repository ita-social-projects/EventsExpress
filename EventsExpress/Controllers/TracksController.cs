using System;
using System.Collections.Generic;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
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

        /// <summary>
        /// This method have to return all tracks.
        /// </summary>
        /// <response code="200">Return IEnumerable ChangeInfo.</response>
        [HttpPost("[action]")]
        public IActionResult All(TrackFilterViewModel filter)
        {
            filter.PageSize = 40;
            //var viewModel = _mapper.Map<IEnumerable<TrackDTO>>(_trackService.GetAllTracks(filter, out int count));
            var viewModel = new IndexViewModel<TrackDTO>
            {
                Items = _mapper.Map<IEnumerable<TrackDTO>>(
                    _trackService.GetAllTracks(filter, out int count)),
                PageViewModel = new PageViewModel(count, filter.Page.Value, filter.PageSize.Value),
            };
            return Ok(viewModel);
        }
    }
}
