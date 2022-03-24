using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventsExpress.Core.DTOs;

namespace EventsExpress.Core.IServices
{
    public interface IEventScheduleManager
    {
        Guid Create(EventScheduleDto eventScheduleDTO);

        Guid CancelEvents(Guid eventId);

        Guid Delete(Guid id);

        Guid CancelNextEvent(Guid eventId);

        Guid Edit(EventScheduleDto eventScheduleDTO);

        EventScheduleDto EventScheduleById(Guid eventScheduleId);

        EventScheduleDto EventScheduleByEventId(Guid eventId);

        IEnumerable<EventScheduleDto> GetAll();

        IEnumerable<EventScheduleDto> GetUrgentEventSchedules();
    }
}
