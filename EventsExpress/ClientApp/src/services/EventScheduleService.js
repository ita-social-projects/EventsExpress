import EventsExpressService from './EventsExpressService';

const baseService = new EventsExpressService();

export default class EventScheduleService {

    getEventSchedule = id =>
        baseService.getResourceNew(`eventSchedule/${id}`);

    getAllEventSchedules = () =>
        baseService.getResourceNew(`eventSchedule/all`);

    setEventSchedule = async (data) => {
        let file = new FormData();

        file.append('Id', data.id);
        file.append('Frequency', data.frequency);
        file.append('LastRun', data.lastRun);
        file.append('NextRun', data.nextRun);
        file.append('Periodicity', data.periodicity);
        file.append('IsActive', data.isActive);

        return await baseService.setResourceWithData(`eventSchedule/${data.eventId}/edit`, file);
    }

    setNextEventScheduleCancel = eventId =>
        baseService.setResourceWithData(`eventSchedule/${eventId}/CancelNextEvent`);

    setEventSchedulesCancel = (eventId) =>
        baseService.setResourceWithData(`eventSchedule/${eventId}/CancelAllEvents`);
}
