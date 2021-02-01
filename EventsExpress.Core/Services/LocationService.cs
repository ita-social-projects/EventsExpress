using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.BaseService;
using EventsExpress.Db.EF;
using EventsExpress.Db.Entities;
using NetTopologySuite.Geometries;

namespace EventsExpress.Core.Services
{
    public class LocationService : BaseService<EventLocation>, ILocationService
    {
        public LocationService(AppDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public async Task<Guid> AddLocationToEvent(LocationDto locationDTO) =>
            LocationByPoint(locationDTO.Point)?.Id ?? await Create(locationDTO);

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
    }
}
