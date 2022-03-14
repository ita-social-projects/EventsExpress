using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Core.IServices;
using EventsExpress.Db.EF;
using EventsExpress.Db.Enums;
using NetTopologySuite.Geometries;

namespace EventsExpress.Core.Services
{
    public class LocationService : BaseService<Db.Entities.Location>, ILocationService
    {
        public LocationService(AppDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public async Task<Guid> AddLocationToEvent(LocationDto locationDto)
        {
            if (locationDto.Type == LocationType.Map)
            {
                return LocationByPoint(locationDto.Point) ?? await Create(new LocationDto { Id = locationDto.Id, Point = locationDto.Point, OnlineMeeting = null, Type = locationDto.Type });
            }
            else
            {
                return LocationByURI(locationDto.OnlineMeeting) ?? await Create(new LocationDto { Id = locationDto.Id, Point = null, OnlineMeeting = locationDto.OnlineMeeting, Type = locationDto.Type });
            }
        }

        public async Task<Guid> Create(LocationDto locationDto)
        {
            var location = Mapper.Map<LocationDto, Db.Entities.Location>(locationDto);

            var result = Insert(location);

            await Context.SaveChangesAsync();

            return result.Id;
        }

        public Guid? LocationByPoint(Point point)
        {
            var locationDto = Mapper.Map<LocationDto>(Context.Locations
                .Where(e =>
                    e.Point.X == point.X &&
                    e.Point.Y == point.Y)
                .FirstOrDefault());
            return locationDto.Id;
        }

        public Guid? LocationByURI(string uri)
        {
            var locationDTO = Mapper.Map<LocationDto>(Context.Locations
                .Where(e =>
                   e.OnlineMeeting == uri)
                .FirstOrDefault());
            return locationDTO.Id;
        }

        public async Task<Guid> AddLocationToUser(LocationDto locationDto)
        {
            if (locationDto.Type == LocationType.Map)
            {
                return LocationByPoint(locationDto.Point) ?? await Create(new LocationDto { Id = locationDto.Id, Point = locationDto.Point, OnlineMeeting = null, Type = locationDto.Type });
            }
            else
            {
                throw new EventsExpressException("Location error");
            }
        }
    }
}
