using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using NetTopologySuite.Geometries;
using Location = EventsExpress.Db.Entities.Location;

namespace EventsExpress.Core.Services
{
    public class LocationService : BaseService<Db.Entities.Location>, ILocationService
    {
        public LocationService(AppDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public async Task<Guid> AddLocationToEvent(LocationDto locationDTO)
        {
            if (locationDTO.Type == LocationType.Map)
            {
                return LocationByPoint(locationDTO.Point)?.Id ?? await Create(new LocationDto { Id = locationDTO.Id, Point = locationDTO.Point, OnlineMeeting = null, Type = locationDTO.Type });
            }
            else
            {
                return LocationByURI(locationDTO.OnlineMeeting)?.Id ?? await Create(new LocationDto { Id = locationDTO.Id, Point = null, OnlineMeeting = locationDTO.OnlineMeeting, Type = locationDTO.Type });
            }
        }

        public async Task<Guid> Create(LocationDto locationDTO)
        {
            var location = Mapper.Map<LocationDto, Db.Entities.Location>(locationDTO);

            var result = Insert(location);

            await Context.SaveChangesAsync();

            return result.Id;
        }

        public LocationDto LocationByPoint(Point point)
        {
            var locationDto = Mapper.Map<LocationDto>(Context.Locations
                .Where(e =>
                    e.Point.X == point.X &&
                    e.Point.Y == point.Y)
                .FirstOrDefault());
            return locationDto;
        }

        public LocationDto LocationByURI(string uri)
        {
            var locationDTO = Mapper.Map<LocationDto>(Context.Locations
                .Where(e =>
                   e.OnlineMeeting == uri)
                .FirstOrDefault());
            return locationDTO;
        }

        public async Task<Guid> AddLocationToUser(LocationDto locationDTO)
        {
            if (locationDTO.Type == LocationType.Map)
            {
                return LocationByPoint(locationDTO.Point)?.Id ?? await Create(new LocationDto { Id = locationDTO.Id, Point = locationDTO.Point, OnlineMeeting = null, Type = locationDTO.Type });
            }
            else
            {
                return LocationByURI(locationDTO.OnlineMeeting)?.Id ?? await Create(new LocationDto { Id = locationDTO.Id, Point = null, OnlineMeeting = locationDTO.OnlineMeeting, Type = locationDTO.Type });
            }
        }
    }
}
