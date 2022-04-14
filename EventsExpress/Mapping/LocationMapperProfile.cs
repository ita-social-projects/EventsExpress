using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Db.Enums;
using EventsExpress.ViewModels.Base;
using NetTopologySuite.Geometries;
using Location = EventsExpress.Db.Entities.Location;

namespace EventsExpress.Mapping
{
    public class LocationMapperProfile : Profile
    {
        public LocationMapperProfile()
        {
            CreateMap<LocationDto, Location>().ReverseMap();
            CreateMap<LocationViewModel, LocationDto>()
                .ForMember(x => x.Id, opts => opts.Ignore())
                .ForMember(x => x.Point, opts => opts.MapFrom(src => MapPoint(src)));
        }

        public Point MapPoint(LocationViewModel l)
        {
            Point location = null;
            location = new Point(l.Latitude.Value, l.Longitude.Value) { SRID = 4326 };
            return location;
        }
    }
}
