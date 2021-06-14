import { CommentService } from '../../services';
import getComments from './comment-list-action';
import { setErrorAllertFromResponse } from '../alert-action';
import { getRequestInc, getRequestDec } from "../request-count-action";

export const SET_COMMENT_DELETE_PENDING = "SET_COMMENT_DELETE_PENDING";
export const SET_COMMENT_DELETE_SUCCESS = "SET_COMMENT_DELETE_SUCCESS";

const api_serv = new CommentService();

export default function delete_comment(data){
    return async dispatch =>{
        dispatch(getRequestInc());
        let response = await api_serv.setCommentDelete(data);
        dispatch(getRequestDec());
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
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