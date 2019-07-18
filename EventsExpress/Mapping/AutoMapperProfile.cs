using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsExpress.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EventDTO, Event>()
              .ForMember(e => e.CityId, ee => ee.MapFrom(e => e.City.Id))
              .ForMember(o => o.OwnerId, oo => oo.MapFrom(o => o.UserId));
        }
    }
}
