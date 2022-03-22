using System;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;
using NetTopologySuite.Geometries;

namespace EventsExpress.Core.IServices
{
    public interface ILocationManager
    {
        Guid Create(LocationDto locationDto);

        void EditLocation(LocationDto locationDto);
    }
}
