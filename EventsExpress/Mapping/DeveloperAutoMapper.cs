using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;

namespace EventsExpress.Mapping
{
    public class DeveloperAutoMapper : Profile
    {
        public DeveloperAutoMapper()
        {
            CreateMap<DeveloperDTO, Developer>()
                .ForMember(dest => dest.Photo, opts => opts.Ignore());
        }
    }
}
