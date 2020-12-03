import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class CommentService {

    setCommentDelete = async (data) => {
        const res = await baseService.setResource(`comment/delete/${data.id}`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setComment = async (data) => {
        const res = await baseService.setResource('comment/edit', {
            Id: data.id,
            Text: data.comment,
            UserId: data.userId,
            EventId: data.eventId,
            CommentsId: data.commentsId
        });
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    getAllComments = async (data, page) => {
        const res = await baseService.getResource(`comment/all/${data}?page=${page}`);
        return res;
    }

}