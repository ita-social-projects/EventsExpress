using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Db.Entities;
using EventsExpress.DTO;

namespace EventsExpress.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDTO>();

            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.PasswordHash, opts => opts.MapFrom(src => src.PasswordHash))
                .ForAllOtherMembers(x => x.Ignore());
                

            CreateMap<LoginDto, UserDTO>();

            CreateMap<UserDTO, UserInfo>()
                .ForMember(dest => dest.Role, opts => opts.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.PhotoUrl, opts => opts.MapFrom(src => src.Photo.Thumb.ToRenderablePictureString()))
                .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender));

            CreateMap<EventDTO, Event>()
              .ForMember(e => e.CityId, ee => ee.MapFrom(e => e.City.Id))
              .ForMember(o => o.OwnerId, oo => oo.MapFrom(o => o.UserId));
        }
    }
}
