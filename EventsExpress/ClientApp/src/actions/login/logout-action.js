import filterHelper from '../../components/helpers/filterHelper';
import { AuthenticationService } from '../../services';
import { reset } from '../chat/chat-action';
import { resetNotification } from '../chat/chats-action';
import { updateEventsFilters } from '../event/event-list-action';

export const SET_LOGOUT = "SET_LOGOUT";

const api_serv = new AuthenticationService();

export default function logout() {
    return async dispatch => {
        dispatch(updateEventsFilters(filterHelper.getDefaultEventFilter()));
        dispatch(reset());
        dispatch(setLogout());
        dispatch(resetNotification());
        localStorage.clear();
        return api_serv.revokeToken();
    }
}

function setLogout() {
    return {
        type: SET_LOGOUT
    };
}
