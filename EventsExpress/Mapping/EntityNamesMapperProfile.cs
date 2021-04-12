using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.ViewModels;

namespace EventsExpress.Mapping
{
    public class EntityNamesMapperProfile : Profile
    {
        public EntityNamesMapperProfile()
        {
            CreateMap<EntityNamesDto, EntityNamesViewModel>();
                /*.ForMember(dest => dest.EntityName, opt => opt.MapFrom(src => new EntityNamesViewModel
                {
                    EntityNameId = src.EntityNameId,
                    EntityName = src.EntityName,
                }));*/
        }
    }
}
