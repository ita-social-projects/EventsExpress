import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class CommentService {

    setCommentDelete = data =>
        baseService.setResource(`comment/${data.id}/delete`);

    setComment = data =>
        baseService.setResource('comment/edit', {
            id: data.id,
            text: data.text,
            userId: data.userId,
            eventId: data.eventId,
            commentsId: data.commentsId
        });

    getAllComments = (data, page) =>
        baseService.getResourceNew(`comment/all/${data}?page=${page}`);
}