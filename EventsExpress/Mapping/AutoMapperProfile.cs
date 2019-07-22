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


            CreateMap<EventDto, EventDTO>()
                .ForMember(dest => dest.CityId, opts => opts.MapFrom(src => src.Location.CityId))
                .ForMember(dest => dest.OwnerId, opts => opts.MapFrom(src => src.User.Id));

            CreateMap<EventDTO, EventDto>()                                                    
                .ForMember(dest => dest.Location, opts => opts.MapFrom(src => new Location() {
                                                            CityId = src.CityId,
                                                            City = src.City.Name,
                                                            Country = src.City.Country.Name
                                                            })) 
                .ForMember(dest => dest.User, opts => opts.MapFrom(src => new UserPreviewDto()
                                                            {
                                                            Id = src.OwnerId,
                                                            Birthday = src.Owner.Birthday,
                                                            //PhotoUrl = src.Owner.Photo.Path,  create default user avatar will be work
                                                            Username = src.Owner.Name
                                                            })); 

            CreateMap<UserDTO, UserPreviewDto>()
                .ForMember(dest => dest.PhotoUrl, opts => opts.MapFrom(src => src.Photo.Path));

            CreateMap<City, Location>()
                .ForMember(dest => dest.City, opts => opts.MapFrom(src => src.Name))            
                .ForMember(dest => dest.Country, opts => opts.MapFrom(src => src.Country.Name)); 

            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.PasswordHash, opts => opts.MapFrom(src => src.PasswordHash))
                .ForAllOtherMembers(x => x.Ignore());
                

            CreateMap<LoginDto, UserDTO>();

            CreateMap<UserDTO, UserInfo>()
                .ForMember(dest => dest.Role, opts => opts.MapFrom(src => src.Role.Name))
                .ForMember(dest => dest.PhotoUrl, opts => opts.MapFrom(src => src.Photo.Thumb.ToRenderablePictureString()))
                .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender));

            CreateMap<EventDTO, Event>().ReverseMap();                                    
        }
    }
}
