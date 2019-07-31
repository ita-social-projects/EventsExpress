import EventsExpressService from '../services/EventsExpressService';
import get_comments from './comment-list';
export const SET_COMMENT_PENDING = "SET_COMMENT_PENDING";
export const SET_COMMENT_SUCCESS = "SET_COMMENT_SUCCESS";
export const SET_COMMENT_ERROR = "SET_COMMENT_ERROR";

const api_serv = new EventsExpressService();

export default function add_comment(data) {

    return dispatch => {
        dispatch(setCommentPending(true));

        const res = api_serv.setComment(data);
        res.then(response => {
            if (response.error == null) {

                dispatch(setCommentSuccess(true));
                dispatch(get_comments(data.eventId));
            } else {
                dispatch(setCommentError(response.error));
            }
        });
    }
}


export function setCommentSuccess(data) {
    return {
        type: SET_COMMENT_SUCCESS,
        payload: data
    };
}

export function setCommentPending(data) {
    return {
        type: SET_COMMENT_PENDING,
        payload: data
    };
}

export function setCommentError(data) {
    return {
        type: SET_COMMENT_ERROR,
        payload: data
    };
}

