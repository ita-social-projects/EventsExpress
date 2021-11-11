using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Policies;
using EventsExpress.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventsExpress.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserMoreInfoController
    {
        private readonly IUserMoreInfoService _userMoreInfoService;
        private readonly IMapper _mapper;

        public UserMoreInfoController(IUserMoreInfoService userService, IMapper mapper)
        {
            _userMoreInfoService = userService;
            _mapper = mapper;
        }

       /* Task<Guid> Create(UserMoreInfoDTO userMoreInfoDTO)
        {

        }

        Task<Guid> Edit(UserMoreInfoDTO userMoreInfoDTO);

        IEnumerable<UserMoreInfoDTO> GetAll();

        UserMoreInfoDTO GetById(Guid userMoreInfoId);

        Task Delete(Guid id);*/
    }
}
