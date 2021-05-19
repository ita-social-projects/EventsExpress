using AutoMapper;
using EventsExpress.Core.DTOs;

using EventsExpress.ViewModels;

namespace EventsExpress.Mapping
{
    public class EntityNameMapperProfile : Profile
    {
        public EntityNameMapperProfile()
        {
            CreateMap<EntityNamesDto, EntityNamesViewModel>()
                .ForMember(dest => dest.EntityName, opts => opts.MapFrom(src => src.EntityName));
        }
    }
}
