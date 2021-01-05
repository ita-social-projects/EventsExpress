import eventHelper from '../components/helpers/eventHelper';
import { reset_hub } from './chat';
import { resetNotification } from './chats';
import { updateEventsFilters } from './event-list';

export const SET_LOGOUT = "SET_LOGOUT";

export default function logout() {
    revokeToken();
    localStorage.clear();
    return dispatch => {
        dispatch(updateEventsFilters(eventHelper.getDefaultEventFilter()));
        dispatch(reset_hub());
        dispatch(setLogout());
        dispatch(resetNotification());
    }
}

function setLogout() {
    return {
        type: SET_LOGOUT
    };
}

function revokeToken() {
    fetch('api/token/revoke-token', {
        method: "POST"
    });
}
