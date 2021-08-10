import initialState from '../store/initialState';
import { INITIAL_CONNECTION, RESET_HUB } from '../actions/chat/chat-action';
import { EVENT_WAS_CREATED } from '../actions/event/event-add-action';
import { SET_USERS_HUB, RESET_USERS_HUB } from "../actions/users/users-action";

export const reducer = (state = initialState.hubConnections, action) => {
    switch (action.type) {
        case INITIAL_CONNECTION:
            return {
                ...state,
                chatHub: action.payload
            }
        case EVENT_WAS_CREATED:
            state.chatHub
                .invoke('EventWasCreated', action.payload)
                .catch(err => console.error(err));
            return state;
        case RESET_HUB:
            return {
                ...state,
                chatHub: null
            }
        case SET_USERS_HUB:
        case RESET_USERS_HUB:
            return {
                ...state,
                usersHub: action.payload
            }
        default:
            return state;
    }
}
