using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using EventsExpress.ViewModels;

namespace EventsExpress.Mapping
{
    public class EntityNamesMapperProfile : Profile
    {
        public EntityNamesMapperProfile()
        {
            CreateMap<ChangeInfo, EntityNamesDto>();
                /*.ForMember(dest => dest.EntityName, opt => opt.MapFrom(src => new EntityNamesDto
                {
                    EntityNameId = src.UserId,
                    EntityName = src.EntityName,
                }));*/

            CreateMap<EntityNamesDto, EntityNamesViewModel>();
                /*.ForMember(dest => dest.EntityName, opt => opt.MapFrom(src => new EntityNamesViewModel
                {
                    EntityNameId = src.EntityNameId,
                    EntityName = src.EntityName,
                }));*/
        }
    }
}
