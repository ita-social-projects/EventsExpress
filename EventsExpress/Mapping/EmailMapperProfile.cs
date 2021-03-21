using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;

namespace EventsExpress.Mapping
{
    public class EmailMapperProfile : Profile
    {
        public EmailMapperProfile()
        {
            CreateMap<EmailDto, EmailMessage>()
                .ReverseMap();
        }
    }
}
