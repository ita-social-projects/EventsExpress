import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class EventService {

    getEvent = async (id) => {
        const res = await baseService.getResource(`event/${id}`);
        return res;
    }

    getAllEvents = async (filters) => {
        const res = await baseService.getResource(`event/all${filters}`);
        return res;
    }

    getEvents = async (eventIds, page) => {
        const res = await baseService.setResource(`event/getEvents?page=${page}`, eventIds);
        return !res.ok
            ? { error: await res.text() }
            : res.json();
    }

    setEventTemplate = async (data, path) => {
        let file = new FormData();
        if (data.id != null) {
            file.append('Id', data.id);
        }

        if (data.image != null) {
            file.append('Photo', data.image.file);
        }

        if (data.isReccurent) {
            file.append('IsReccurent', data.isReccurent);
            file.append('Frequency', data.frequency);
            file.append('Periodicity', data.periodicity);
        }

        if (data.photoId) {
            file.append('PhotoId', data.photoId);
        }

        if(data.selectedPos) {
            file.append('Latitude', data.selectedPos.lat);
            file.append('Longitude', data.selectedPos.lng);
        }

        file.append('Title', data.title);
        file.append('Description', data.description);
        file.append('User.Id', data.user_id);
        file.append('IsPublic', data.isPublic);
        file.append('MaxParticipants', data.maxParticipants);
        file.append('DateFrom', new Date(data.dateFrom).toDateString());
        file.append('DateTo', new Date(data.dateTo).toDateString());

        if (data.inventories) {
            data.inventories.map((item, key) => {
                file.append(`Inventories[${key}].NeedQuantity`, item.needQuantity);
                file.append(`Inventories[${key}].ItemName`, item.itemName);
                file.append(`Inventories[${key}].UnitOfMeasuring.id`, item.unitOfMeasuring.id);
            });
        }

        let i = 0;
        data.categories.map(x => {
            return file.append(`Categories[${i++}].Id`, x.id);
        });
        const res = await baseService.setResourceWithData(path, file);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setEvent = async (data) => {
        return this.setEventTemplate(data, `event/create`)
    }

    setCopyEvent = async (eventId) => {
        const res = await baseService.setResourceWithData(`event/CreateNextFromParent/${eventId}`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setEventFromParent = async (data) => {
        return this.setEventTemplate(data,`event/CreateNextFromParentWithEdit/${data.id}`);
    }

    editEvent = async(data) => {
        return this.setEventTemplate(data,`event/${data.id}/edit`)
    }

    setEventCancel = async (data) => {
        const res = await baseService.setResource(`EventStatusHistory/${data.EventId}/Cancel`, data);
        return !res.ok
            ? { error: await res.text() }
            : await res.json();
    }

    setUserToEvent = async (data) => {
        const res = await baseService.setResource(`event/${data.eventId}/AddUserToEvent?userId=${data.userId}`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setUserFromEvent = async (data) => {
        const res = await baseService.setResource(`event/${data.eventId}/DeleteUserFromEvent?userId=${data.userId}`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setApprovedUser = async (data) => {
        const res = data.buttonAction
            ? await baseService.setResource(`event/${data.eventId}/ApproveVisitor?userId=${data.userId}`)
            : await baseService.setResource(`event/${data.eventId}/DenyVisitor?userId=${data.userId}`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    onDeleteFromOwners = async (data) => {
        const res = await baseService.setResource(`owners/DeleteFromOwners?userId=${data.userId}&eventId=${data.eventId}`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    onPromoteToOwner = async (data) => {
        const res = await baseService.setResource(`owners/PromoteToOwner?userId=${data.userId}&eventId=${data.eventId}`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setRate = async (data) => {
        const res = await baseService.setResource('event/setrate', {
            rate: data.rate,
            userId: data.userId,
            eventId: data.eventId
        });
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    getCurrentRate = async (eventId) => {
        const res = await baseService.getResource(`event/${eventId}/GetCurrentRate`);
        return res;
    }

    getAverageRate = async (eventId) => {
        const res = await baseService.getResource(`event/${eventId}/GetAverageRate`);
        return res;
    }

    setEventBlock = async (id) => {
        const res = await baseService.setResource(`Event/${id}/Block`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setEventUnblock = async (id) => {
        const res = await baseService.setResource(`event/${id}/Unblock`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    getFutureEvents = async (id, page) => {
        const res = await baseService.getResource(`event/futureEvents?id=${id}&page=${page}`);
        return res;
    }

    getPastEvents = async (id, page) => {
        const res = await baseService.getResource(`event/pastEvents?id=${id}&page=${page}`);
        return res;
    }

    getEventsToGo = async (id, page) => {
        const res = await baseService.getResource(`event/EventsToGo?id=${id}&page=${page}`);
        return res;
    }

    getVisitedEvents = async (id, page) => {
        const res = await baseService.getResource(`event/visitedEvents?id=${id}&page=${page}`);
        return res;
    }
}