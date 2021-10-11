import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class EventService {

    getEvent = id => baseService.getResource(`event/${id}`);

    getAllEvents = filters => baseService.getResource(`event/all${filters}`);

    getAllDrafts = (page) => baseService.getResource(`event/AllDraft/${page}`);
          
    getEvents = (eventIds, page) => baseService.setResource(`event/getEvents?page=${page}`, eventIds);

    setEvent = data => baseService.setResource(`event/create`, data);
    
    setCopyEvent = eventId =>
        baseService.setResourceWithData(`event/CreateNextFromParent/${eventId}`);

    setEventFromParent = async (data) => {
        return baseService.setResource( `event/CreateNextFromParentWithEdit/${data.id}`, data);
    }
    
    editEvent = async (data) => {
        return baseService.setResource(`event/${data.id}/edit`, data)
    }
    publishEvent = (id, data) => {
        return baseService.setResource(`event/${id}/publish`, data)
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
