import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class CommentService {

    setCommentDelete = async (data) => {
        const res = await baseService.setResource(`comment/${data.id}/delete`);
        return !res.ok
            ? { error: await res.text() }
            : res;
    }

    setComment = async (data) => {
        const res = await baseService.setResource('comment/edit', {
            id: data.id,
            text: data.text,
            userId: data.userId,
            eventId: data.eventId,
            commentsId: data.commentsId
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