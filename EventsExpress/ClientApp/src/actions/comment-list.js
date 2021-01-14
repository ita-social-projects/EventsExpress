import { CommentService } from '../services';
import { setAlert } from './alert';


export const SET_COMMENTS_PENDING = "SET_COMMENTS_PENDING";
export const GET_COMMENTS_SUCCESS = "GET_COMMENTS_SUCCESS";
export const SET_COMMENTS_ERROR = "SET_COMMENTS_ERROR";
export const SET_COMMENT_DELETE_ERROR = "SET_COMMENT_DELETE_ERROR";

const api_serv = new CommentService();

export default function get_comments(data, page) {
    return dispatch => {
        dispatch(setCommentPending(true));
        const res = api_serv.getAllComments(data, page);
        return res.then(response => {
            if (response.error == null) {
                dispatch(getComments(response));
            } else {
                dispatch(setAlert({ variant: 'error', message: 'Some error' }));
            }
        });
    }
}

function setCommentPending(data) {
    return {
        type: SET_COMMENTS_PENDING,
        payload: data
    }
}

function getComments(data) {
    return {
        type: GET_COMMENTS_SUCCESS,
        payload: data
    }
}