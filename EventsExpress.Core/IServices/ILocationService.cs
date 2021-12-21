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
        Task<Guid> AddLocationToEvent(LocationDto locationDTO);

        Task<Guid> Create(LocationDto locationDTO);

        LocationDto LocationByPoint(Point point);

        LocationDto LocationByURI(string uri);
    }
}
