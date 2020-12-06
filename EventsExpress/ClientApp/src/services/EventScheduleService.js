import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class EventScheduleService {

    getEventSchedule = async (id) => {
        const res = await baseService.getResource(`eventSchedule/${id}`);
        return res;
    }

    getAllEventSchedules = async () => {
        const res = await baseService.getResource(`eventSchedule/all`);
        return res;
    }

    setEventSchedule = async (data) => {
        let file = new FormData();

        file.append('Id', data.id);
        file.append('Frequency', data.frequency);
        file.append('LastRun', data.lastRun);
        file.append('NextRun', data.nextRun);
        file.append('Periodicity', data.periodicity);
        file.append('IsActive', data.isActive);

        const res = await baseService.setResourceWithData(`eventSchedule/${data.eventId}/edit`, file);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setNextEventScheduleCancel = async (eventId) => {
        const res = await baseService.setResourceWithData(`eventSchedule/${eventId}/CancelNextEvent`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setEventSchedulesCancel = async (eventId) => {
        const res = await baseService.setResourceWithData(`eventSchedule/${eventId}/CancelAllEvents`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }
}
