import { CommentService } from '../services';
import get_comments from './comment-list';

export const SET_COMMENT_DELETE_PENDING = "SET_COMMENT_DELETE_PENDING";
export const SET_COMMENT_DELETE_SUCCESS = "SET_COMMENT_DELETE_SUCCESS";
export const SET_COMMENT_DELETE_ERROR = "SET_COMMENT_DELETE_ERROR";

export default function delete_comment(data) {

    const api_serv = new CommentService();

    return dispatch => {
        dispatch(setCommentPending(true));

        const res = api_serv.setCommentDelete(data);
        res.then(response => {
            if (response.error == null) {

                dispatch(setCommentSuccess(true));
                dispatch(get_comments(data.eventId, 1));
            } else {
                dispatch(set3CommentError(response.error));
            }
        });
    }
}


function setCommentSuccess(data) {
    return {
        type: SET_COMMENT_DELETE_SUCCESS,
        payload: data
    };
}

function setCommentPending(data) {
    return {
        type: SET_COMMENT_DELETE_PENDING,
        payload: data
    };
}

export function set3CommentError(data) {
    return {
        type: SET_COMMENT_DELETE_ERROR,
        payload: data
    };
}

