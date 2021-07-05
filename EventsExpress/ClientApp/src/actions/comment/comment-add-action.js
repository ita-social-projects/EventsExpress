import getComments from './comment-list-action';
import { CommentService } from '../../services';
import { SubmissionError, reset } from 'redux-form';
import { buildValidationState } from '../../components/helpers/action-helpers';
import { getRequestInc, getRequestDec } from "../request-count-action";

const api_serv = new CommentService();

export default function (data) {
    return async dispatch => {
        dispatch(getRequestInc());
        let response = await api_serv.setComment(data);
        dispatch(getRequestDec());
        if (!response.ok) {
            throw new SubmissionError(await buildValidationState(response));
        }
        dispatch(getComments(data.eventId));
        dispatch(reset('add-comment'));
        return Promise.resolve();
    }
}
