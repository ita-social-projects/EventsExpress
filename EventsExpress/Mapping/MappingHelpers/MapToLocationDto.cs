using System;
using EventsExpress.Core.DTOs;
using EventsExpress.Core.Exceptions;
using EventsExpress.Db.Enums;
using EventsExpress.ViewModels;
using NetTopologySuite.Geometries;

namespace EventsExpress.Mapping.MappingHelpers
{
    public static class MapToLocationDto<T>
    {
        public delegate Point PointAction(T e);

        public static LocationDto Map(T eventViewModel, PointAction pointAction)
        {
            dynamic e;
            if (typeof(T) == typeof(EventCreateViewModel))
            {
                e = eventViewModel as EventCreateViewModel;
            }
            else if (typeof(T) == typeof(EventEditViewModel))
            {
                e = eventViewModel as EventEditViewModel;
            }
            else
            {
                throw new EventsExpressException("Cannot cast generic T to any type of Event View Model in Map to Location Dto");
            }

            var locationDto = new LocationDto();
            if (e.Location != null)
            {
                if (e.Location.Type != null)
                {
                    locationDto.Type = e.Location.Type;
                }

                if (e.Location.OnlineMeeting != null && e.Location.Type == LocationType.Online)
                {
                    locationDto.OnlineMeeting = e.Location.OnlineMeeting;
                }

                if (e.Location.Type == LocationType.Map)
                {
                    locationDto.Point = pointAction.Invoke(e);
                }
            }

            return locationDto;
        }
    }
}
