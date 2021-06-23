import { EventService } from '../../services';
import { setErrorAllertFromResponse, setSuccessAllert } from '../alert-action';
import { createBrowserHistory } from 'history';
import { getRequestInc, getRequestDec } from "../request-count-action";

const api_serv = new EventService();
const history = createBrowserHistory({ forceRefresh: true });

export default function add_copy_event(eventId) {
    return async dispatch => {
        dispatch(getRequestInc());
        let response = await api_serv.setCopyEvent(eventId);
        dispatch(getRequestDec())
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(setSuccessAllert('Your event has been successfully created!'));
        dispatch(history.push(`/event/${jsonRes.id}/1`));
        return Promise.resolve();
    }
}