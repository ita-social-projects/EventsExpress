using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Extensions;
using EventsExpress.Db.Entities;
using EventsExpress.DTO;

namespace EventsExpress.Mapping
{
    public class TeamAutoMapper : Profile
    {
        public TeamAutoMapper()
        {
            CreateMap<Team, TeamDto>()
                .ForMember(dest => dest.Developers, opts => opts.MapFrom(src => src.Developers
                    .Select(x => new DeveloperDto
                    {
                        Name = x.Name,
                        Description = x.Description,
                        Photo = x.Photo.Thumb.ToRenderablePictureString(),
                    })))
                .ForMember(dest => dest.Photos, opts => opts.MapFrom(src => src.TeamPhotos
                    .Select(x => x.Photo.Thumb.ToRenderablePictureString())))
                ;

            CreateMap<TeamDTO, Team>();
        }
    }
}
