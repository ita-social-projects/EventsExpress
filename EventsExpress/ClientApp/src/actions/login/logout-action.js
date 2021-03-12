import eventHelper from '../../components/helpers/eventHelper';
import { AuthenticationService } from '../../services';
import { reset_hub } from '../chat/chat';
import { resetNotification } from '../chat/chats';
import { updateEventsFilters } from '../event/event-list-action';

export const SET_LOGOUT = "SET_LOGOUT";

const api_serv = new AuthenticationService();

export default function logout() {
    return async dispatch => {
        dispatch(updateEventsFilters(eventHelper.getDefaultEventFilter()));
        dispatch(reset_hub());
        dispatch(setLogout());
        dispatch(resetNotification());
        localStorage.clear();
        return await api_serv.revokeToken();
    }
}

function setLogout() {
    return {
        type: SET_LOGOUT
    };
}
