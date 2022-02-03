import EventsExpressService from './EventsExpressService';

const baseService = new EventsExpressService();

export default class BookmarkService {
    saveEventBookmark = (userId, eventId) => baseService.setResource(
        `Bookmark/SaveEventBookmark?userId=${userId}&eventId=${eventId}`
    );

    deleteEventBookmark = (userId, eventId) => baseService.setResource(
        `Bookmark/DeleteEventBookmark?userId=${userId}&eventId=${eventId}`
    );
}
