import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class EventService {

    getEvent = id => baseService.getResource(`event/${id}`);

    getAllEvents = filters => baseService.getResource(`event/all${filters}`);

    getAllDrafts = (page) => baseService.getResource(`event/AllDraft/${page}`);
          
    getEvents = (eventIds, page) => baseService.setResource(`event/getEvents?page=${page}`, eventIds);

    setEventTemplate = (data, path) => {
        let file = new FormData();
        if (data.id != null) {
            file.append('Id', data.id);
        }


        file.append('EventStatus', data.eventStatus);
        
        if (data.isReccurent) {
            file.append('IsReccurent', data.isReccurent);
            file.append('Periodicity', data.periodicity);
            file.append('Frequency', data.frequency);
        }

        if (data.location) {
            file.append('Location.Type', data.location.type)
            if (data.location.latitude) {
                file.append('Location.Latitude', data.location.latitude);
                file.append('Location.Longitude', data.location.longitude);
            }
            if (data.location.onlineMeeting) {
                file.append('Location.OnlineMeeting', data.location.onlineMeeting);
            }
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
        if (data.categories != null) {
            data.categories.map(x => {
                return file.append(`Categories[${i++}].Id`, x.id);
            });
        }
           
        return baseService.setResourceWithData(path, file);
    }

    setEvent = data => this.setEventTemplate(data, `event/create`);
    

    setCopyEvent = eventId =>
        baseService.setResourceWithData(`event/CreateNextFromParent/${eventId}`);

    setEventFromParent = async (data) => {
        let photo = new FormData();
        photo.append(`Photo`, data.photo.file);
        await baseService.setResourceWithData(`event/SetEventTempPhoto/${data.id}`, photo);
        this.setEventTemplate(data, `event/CreateNextFromParentWithEdit/${data.id}`);
    }
    
    editEvent = async (data) => {
        let photo = new FormData();
        photo.append(`Photo`, data.photo.file);
        await baseService.setResourceWithData(`event/SetEventTempPhoto/${data.id}`, photo);
        return this.setEventTemplate(data,`event/${data.id}/edit`)
    }
    publishEvent = (id) => {
       return baseService.setResource(`event/${id}/publish`)
    }

    setEventStatus = data =>  baseService.setResource(`EventStatusHistory/${data.EventId}/SetStatus`, data);

    setUserToEvent = data => baseService.setResource(`event/${data.eventId}/AddUserToEvent?userId=${data.userId}`);

    setUserFromEvent = data => baseService.setResource(`event/${data.eventId}/DeleteUserFromEvent?userId=${data.userId}`);

    setApprovedUser = data => data.buttonAction
            ?  baseService.setResource(`event/${data.eventId}/ApproveVisitor?userId=${data.userId}`)
            :  baseService.setResource(`event/${data.eventId}/DenyVisitor?userId=${data.userId}`);

    onDeleteFromOwners = data => baseService.setResource(`owners/DeleteFromOwners?userId=${data.userId}&eventId=${data.eventId}`);

    onPromoteToOwner = data => baseService.setResource(`owners/PromoteToOwner?userId=${data.userId}&eventId=${data.eventId}`);

    setRate = data =>  baseService.setResource('event/setrate', {
            rate: Number(data.rate),
            userId: data.userId,
            eventId: data.eventId
        });

    getCurrentRate = eventId => baseService.getResource(`event/${eventId}/GetCurrentRate`);

    getAverageRate = eventId => baseService.getResource(`event/${eventId}/GetAverageRate`);
    
    getFutureEvents = async (id, page) =>
        baseService.getResource(`event/futureEvents?id=${id}&page=${page}`);

    getPastEvents = (id, page) =>
        baseService.getResource(`event/pastEvents?id=${id}&page=${page}`);

    getEventsToGo = (id, page) =>
        baseService.getResource(`event/EventsToGo?id=${id}&page=${page}`);

    getVisitedEvents = (id, page) =>
        baseService.getResource(`event/visitedEvents?id=${id}&page=${page}`);
}
