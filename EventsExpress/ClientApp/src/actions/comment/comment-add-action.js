import getComments from './comment-list-action';
import { CommentService } from '../../services';
import { SubmissionError, reset } from 'redux-form';
import { buildValidationState } from '../../components/helpers/action-helpers';

export const SET_COMMENT_PENDING = "SET_COMMENT_PENDING";
export const SET_COMMENT_SUCCESS = "SET_COMMENT_SUCCESS";

const api_serv = new CommentService();

export default function addComment(data) {
    return async dispatch => {
        dispatch(setCommentPending(true));
        let response = await api_serv.setComment(data);
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(getComments(data.eventId));
        dispatch(reset('add-comment'));
        return Promise.resolve();
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
