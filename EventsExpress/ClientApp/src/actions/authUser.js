import { setLoginSuccess, setUser } from './login/login-action';
import { initialConnection } from './chat/chat-action';
import { getUnreadMessages } from './chat/chats-action';
import { updateEventsFilters } from './event/event-list-action';
import eventHelper from './../components/helpers/eventHelper';
import { AuthenticationService } from '../services';

const api_serv = new AuthenticationService();

export default function AuthUser() {
    return async dispatch => {
        if (!api_serv.getCurrentToken())
            return;

        const res = await api_serv.setAuth();

        console.log('response');

        if (res.ok) {
            const user = await res.json();
            const eventFilter = {
                ...eventHelper.getDefaultEventFilter(),
                categories: user.categories.map(item => item.id),
            }

            dispatch(setLoginSuccess(true))
            dispatch(setUser(user))
            dispatch(updateEventsFilters(eventFilter))
            dispatch(initialConnection())
            dispatch(getUnreadMessages(user.id))
        } else {
            localStorage.clear();
        }
    }
}