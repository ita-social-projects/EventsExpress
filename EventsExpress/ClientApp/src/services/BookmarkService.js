import EventsExpressService from './EventsExpressService';

const baseService = new EventsExpressService();

export default class BookmarkService {
    saveEventBookmark = eventId => baseService.setResource(
        `Bookmark/SaveEventBookmark?eventId=${eventId}`
    );

    deleteEventBookmark = eventId => baseService.setResource(
        `Bookmark/DeleteEventBookmark?eventId=${eventId}`
    );
}
