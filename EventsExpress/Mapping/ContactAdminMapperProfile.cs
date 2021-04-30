using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Entities;
using EventsExpress.ViewModels;

namespace EventsExpress.Mapping
{
    public class ContactAdminMapperProfile : Profile
    {
        public ContactAdminMapperProfile()
        {
            CreateMap<ContactAdmin, ContactAdminDto>()
                .ForMember(dest => dest.MessageText, opt => opt.MapFrom(src => src.EmailBody));

            CreateMap<ContactAdminDto, ContactAdmin>()
                .ForMember(dest => dest.EmailBody, opt => opt.MapFrom(src => src.MessageText))
                .ForMember(dest => dest.Assignee, opt => opt.Ignore())
                .ForMember(dest => dest.AssigneeId, opts => opts.Ignore())
                .ForMember(dest => dest.DateUpdated, opts => opts.Ignore())
                .ForMember(dest => dest.ResolutionDetails, opts => opts.Ignore())
                .ForMember(dest => dest.Sender, opts => opts.Ignore());

            CreateMap<ContactAdminDto, ContactUsViewModel>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.MessageText));

            CreateMap<ContactUsViewModel, ContactAdminDto>()
                .ForMember(dest => dest.MessageText, opt => opt.MapFrom(src => src.Description));
        }
    }
}
