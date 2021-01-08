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

        public async Task<Guid> Create(LocationDTO locationDTO)
        {
            var location = _mapper.Map<LocationDTO, EventLocation>(locationDTO);

            var result = Insert(location);

            await _context.SaveChangesAsync();

            return result.Id;
        }

        public async Task<Guid> Edit(LocationDTO locationDTO)
        {
            var location = _context.EventLocations
                .Find(locationDTO.Id);

            location.Point = locationDTO.Point;

            await _context.SaveChangesAsync();

            return location.Id;
        }

        public LocationDTO LocationById(Guid locationId) =>
            _mapper.Map<LocationDTO>(_context.EventLocations.Find(locationId));

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
