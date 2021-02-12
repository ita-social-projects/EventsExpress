import { CommentService } from '../services';
import getComments from './comment-list';
import { setErrorAllertFromResponse } from './alert-action';

export const SET_COMMENT_DELETE_PENDING = "SET_COMMENT_DELETE_PENDING";
export const SET_COMMENT_DELETE_SUCCESS = "SET_COMMENT_DELETE_SUCCESS";

const api_serv = new CommentService();

export default function delete_comment(data){
    return async dispatch =>{
        dispatch(setCommentPending(true));
        let response = await api_serv.setCommentDelete(data);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        dispatch(setCommentSuccess(true));
        dispatch(getComments(data.eventId));
        return Promise.resolve;
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