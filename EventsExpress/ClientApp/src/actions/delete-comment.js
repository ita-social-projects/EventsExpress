import { CommentService } from '../services';
import get_comments from './comment-list';
import { setAlert } from './alert';
export const SET_COMMENT_DELETE_PENDING = "SET_COMMENT_DELETE_PENDING";
export const SET_COMMENT_DELETE_SUCCESS = "SET_COMMENT_DELETE_SUCCESS";
export const SET_COMMENT_DELETE_ERROR = "SET_COMMENT_DELETE_ERROR";

const api_serv = new CommentService();

export default function delete_comment(data) {
    return dispatch => {
        dispatch(setCommentPending(true));
        data.id = '08a7cd52-475f-4505-55a3-08d8b0fd6aad';
        const res = api_serv.setCommentDelete(data);
        return res.then(response => {
            if (response.error == null) {

                dispatch(setCommentSuccess(true));
                dispatch(get_comments(data.eventId, 1));
            } else {
                dispatch(setAlert({ variant: 'error', message: 'Some error' }));
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