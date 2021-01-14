using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using NetTopologySuite.Geometries;

namespace EventsExpress.Core.IServices
{
    public interface ILocationService
    {
        Task<Guid> AddLocationToEvent(LocationDTO locationDTO);

        Task<Guid> Create(LocationDTO locationDTO);

        LocationDTO LocationByPoint(Point point);
    }
}
