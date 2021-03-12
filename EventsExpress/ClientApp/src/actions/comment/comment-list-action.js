import { CommentService } from '../../services';
import { setErrorAllertFromResponse } from '../alert-action';

export const SET_COMMENTS_PENDING = "SET_COMMENTS_PENDING";
export const GET_COMMENTS_SUCCESS = "GET_COMMENTS_SUCCESS";

const api_serv = new CommentService();

export default function getComments(data, page = 1){
    return async dispatch => {
        dispatch(setCommentPending(true));
        let response = await api_serv.getAllComments(data, page);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getCommentsInternal(jsonRes));
        return Promise.resolve();
    }
}

function setCommentPending(data) {
    return {
        type: SET_COMMENTS_PENDING,
        payload: data
    }
}

function getCommentsInternal(data) {
    return {
        type: GET_COMMENTS_SUCCESS,
        payload: data
    }
}
