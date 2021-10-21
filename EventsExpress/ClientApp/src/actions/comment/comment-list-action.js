import { CommentService } from '../../services';
import { setErrorAllertFromResponse } from '../alert-action';
import { getRequestLocalInc, getRequestLocalDec } from "../request-local-count-action";

export const GET_COMMENTS_DATA = "GET_COMMENTS_DATA";

const api_serv = new CommentService();

export default function getComments(data, page = 1){
    return async dispatch => {
        dispatch(getRequestLocalInc());
        let response = await api_serv.getAllComments(data, page);
        dispatch(getRequestLocalDec());
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getCommentsInternal(jsonRes));
        return Promise.resolve();
    }
}

function getCommentsInternal(data) {
    return {
        type: GET_COMMENTS_DATA,
        payload: data
    }
}
