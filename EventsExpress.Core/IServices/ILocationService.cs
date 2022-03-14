using System;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using NetTopologySuite.Geometries;

namespace EventsExpress.Core.IServices
{
    public interface ILocationService
    {
        Task<Guid> AddLocationToEvent(LocationDto locationDto);

        Task<Guid> Create(LocationDto locationDto);

        Task<Guid> AddLocationToUser(LocationDto locationDto);

        Guid? LocationByPoint(Point point);

        Guid? LocationByURI(string uri);
    }
}
