using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Infrastructure;
using EventsExpress.Db.Entities;
using EventsExpress.DTO;
using System.Collections.Generic;
using System.Linq;

namespace EventsExpress.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDTO>();

            CreateMap<CategoryDTO, Category>().ReverseMap();
            CreateMap<CategoryDto, CategoryDTO>().ReverseMap();

            CreateMap<EventDto, EventDTO>()
                .ForMember(dest => dest.CityId, opts => opts.MapFrom(src => src.CityId))
                .ForMember(dest => dest.OwnerId, opts => opts.MapFrom(src => src.User.Id));

            CreateMap<EventDTO, EventDto>()

                .ForMember(dest => dest.PhotoUrl, opts => opts.MapFrom(src => src.PhotoBytes.ToRenderablePictureString()))
                .ForMember(dest => dest.Visitors, opts => opts.MapFrom(src => src.Visitors.Select(x => new UserPreviewDto {
                    Id = x.User.Id,
                    Username = x.User.Name,
                    Birthday = x.User.Birthday,
                    PhotoUrl = x.User.Photo != null ? x.User.Photo.Thumb.ToRenderablePictureString() : null 
                    })))
                .ForMember(dest => dest.Country, opts => opts.MapFrom(src => src.City.Country.Name)) 
                .ForMember(dest => dest.CountryId, opts => opts.MapFrom(src => src.City.Country.Id)) 
                .ForMember(dest => dest.City, opts => opts.MapFrom(src => src.City.Name)) 
                .ForMember(dest => dest.CityId, opts => opts.MapFrom(src => src.City.Id)) 
                .ForMember(dest => dest.User, opts => opts.MapFrom(src => new UserPreviewDto()
                                                            {
                                                            Id = src.OwnerId,
                                                            Birthday = src.Owner.Birthday,
                                                            PhotoUrl = src.Owner.Photo != null? src.Owner.Photo.Thumb.ToRenderablePictureString() : null,
                                                            Username = src.Owner.Name
                                                            })); 


            CreateMap<UserDTO, UserPreviewDto>()
                .ForMember(dest => dest.PhotoUrl, opts => opts.MapFrom(src => src.Photo.Thumb.ToRenderablePictureString()));

            CreateMap<UserDTO, UserManageDto>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.Username, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsBlocked, opts => opts.MapFrom(src => src.IsBlocked))
                .ForMember(dest => dest.Role, opts => opts.MapFrom(src => new RoleDto { Id = src.RoleId, Name = src.Role.Name }))
                .ForMember(dest => dest.PhotoUrl, opts => opts.MapFrom(src => src.Photo.Thumb.ToRenderablePictureString()));

            //CreateMap<City, Location>()
            //    .ForMember(dest => dest.City, opts => opts.MapFrom(src => src.Name))            
            //    .ForMember(dest => dest.Country, opts => opts.MapFrom(src => src.Country.Name)); 

            CreateMap<UserDTO, User>()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.EmailConfirmed, opts => opts.MapFrom(src => src.EmailConfirmed))
                .ForMember(dest => dest.Birthday, opts => opts.MapFrom(src => src.Birthday))
                .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender))
                .ForMember(dest => dest.IsBlocked, opts => opts.MapFrom(src => src.IsBlocked))
                .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
                .ForMember(dest => dest.PasswordHash, opts => opts.MapFrom(src => src.PasswordHash))
                .ForMember(dest => dest.PhotoId, opts => opts.MapFrom(src => src.PhotoId))
                .ForMember(dest => dest.RoleId, opts => opts.MapFrom(src => src.RoleId))
                .ForMember(dest => dest.Categories, opts => opts.MapFrom(src => src.Categories))
                .ForAllOtherMembers(x => x.Ignore());



            CreateMap<LoginDto, UserDTO>();

            CreateMap<UserDTO, UserInfo>()
                .ForMember(dest => dest.Role, opts => opts.MapFrom(src => src.Role.Name))
                                                                                          
                .ForMember(dest => dest.Categories, opts => opts.MapFrom(src =>  src.Categories.Select(x => new CategoryDto { Id = x.Category.Id, Name = x.Category.Name})))
                .ForMember(dest => dest.PhotoUrl, opts => opts.MapFrom(src => src.Photo.Thumb.ToRenderablePictureString()))
                .ForMember(dest => dest.Gender, opts => opts.MapFrom(src => src.Gender));

            CreateMap<EventDTO, Event>()
                .ForMember(dest => dest.Photo, opt => opt.Ignore())
                .ForMember(dest => dest.Visitors, opt => opt.Ignore())
                .ForMember(dest => dest.Categories, opt => opt.Ignore());

            CreateMap<Event, EventDTO>()
                .ForMember(dest => dest.Photo, opt => opt.Ignore())
                .ForMember(dest => dest.Categories, opts => opts.MapFrom(src => src.Categories.Select(x => new CategoryDto { Id = x.Category.Id, Name = x.Category.Name })))
                .ForMember(dest => dest.PhotoBytes, opt => opt.MapFrom(src => src.Photo.Thumb));

            CreateMap<UserDTO, UserPreviewDto>()
                .ForMember(dest => dest.Username, opts => opts.MapFrom(src => src.Name))
                .ForMember(dest => dest.PhotoUrl, opts => opts.MapFrom(src => src.Photo.Thumb.ToRenderablePictureString()));
                                                         

            CreateMap<CategoryDto, Category>();


            CreateMap<CommentDTO, Comments>().ReverseMap();
            CreateMap<CommentDto, CommentDTO>();
            CreateMap<CommentDTO, CommentDto>()
                .ForMember(dest => dest.UserPhoto, opts => opts.MapFrom(src => src.User.Photo.Thumb.ToRenderablePictureString()))            
                .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.User.Name));

            CreateMap<Role, RoleDto>();
        }
    }
}
