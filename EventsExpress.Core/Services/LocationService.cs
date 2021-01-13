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

        public async Task<Guid> AddLocationToEvent(LocationDTO locationDTO)
        {
            var locationDTOByLatLng = LocationByPoint(locationDTO.Point);
            var locationId = Guid.Empty;
            if (locationDTOByLatLng != null)
            {
                locationId = locationDTOByLatLng.Id;
            }
            else
            {
                locationId = await Create(locationDTO);
            }

            return locationId;
        }

        public async Task<Guid> Create(LocationDTO locationDTO)
        {
            var location = _mapper.Map<LocationDTO, EventLocation>(locationDTO);

            var result = Insert(location);

            await _context.SaveChangesAsync();

            return result.Id;
        }

        public LocationDTO LocationByPoint(Point point)
        {
            var locationDTO = _mapper.Map<LocationDTO>(_context.EventLocations
                .Where(e =>
                    e.Point.X == point.X &&
                    e.Point.Y == point.Y)
                .FirstOrDefault());
            return locationDTO;
        }
    }
}
