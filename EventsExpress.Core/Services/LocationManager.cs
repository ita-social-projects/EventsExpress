using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.IServices;
using EventsExpress.Db.EF;
using Microsoft.EntityFrameworkCore;

namespace EventsExpress.Core.Services
{
    public class LocationManager : BaseService<Db.Entities.Location>, ILocationManager
    {
        public LocationManager(AppDbContext context, IMapper mapper)
            : base(context, mapper)
        {
        }

        public Guid Create(LocationDto locationDto)
        {
            var location = Mapper.Map<LocationDto, Db.Entities.Location>(locationDto);

            var result = Insert(location);

            return result.Id;
        }

        public void EditLocation(LocationDto locationDto)
        {
            var location = Mapper.Map<LocationDto, Db.Entities.Location>(locationDto);

            var result = Update(location);
        }
    }
}
