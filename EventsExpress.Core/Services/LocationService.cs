using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.BaseService;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using EventsExpress.Db.Enums;
using NetTopologySuite.Geometries;

namespace EventsExpress.Core.Services
{
    public class LocationService : BaseService<EventLocation>, ILocationService
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
            var location = Mapper.Map<LocationDto, EventLocation>(locationDTO);

            var result = Insert(location);

            await Context.SaveChangesAsync();

            return result.Id;
        }

        public LocationDto LocationByPoint(Point point)
        {
            var locationDTO = Mapper.Map<LocationDto>(Context.EventLocations
                .Where(e =>
                    e.Point.X == point.X &&
                    e.Point.Y == point.Y)
                .FirstOrDefault());
            return locationDTO;
        }

        public LocationDto LocationByURI(Uri uri)
        {
            var locationDTO = Mapper.Map<LocationDto>(Context.EventLocations
                .Where(e =>
                   e.OnlineMeeting == uri)
                .FirstOrDefault());
            return locationDTO;
        }
    }
}
