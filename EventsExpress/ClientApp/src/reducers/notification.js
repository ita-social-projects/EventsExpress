
import initialState from '../store/initialState';
import {
     RECEIVE_MESSAGE, DELETE_OLD_NOTIFICATION, RECEIVE_SEEN_MESSAGE, DELETE_SEEN_MSG_NOTIFICATION, RECEIVED_NEW_EVENT
}from '../actions/chat';

import {
    GET_UNREAD_MESSAGES, RESET_NOTIFICATION
}from '../actions/chats';


export const reducer = (
    state = initialState.notification,
    action
  ) => {
    switch (action.type) {
        case RECEIVED_NEW_EVENT: 
            var new_events = state.events;
            new_events = new_events.concat(action.payload);
            return {
                ...state,
                events: new_events
            }
        case RECEIVE_MESSAGE:
            var new_msg = state.messages;
            new_msg = new_msg.concat(action.payload);
            return {
                ...state,
                messages: new_msg
            }
        case GET_UNREAD_MESSAGES:
            return {
                ...state,
                messages: action.payload
            }
        case RECEIVE_SEEN_MESSAGE:
                var new_msg = state.seen_messages;
                new_msg = new_msg.concat(action.payload);
            return {
                ...state,
                seen_messages: new_msg
            }
        case DELETE_SEEN_MSG_NOTIFICATION:
                var new_msg = state.seen_messages;
                new_msg = new_msg.filter(x => x.id != action.payload);
            return {
                ...state,
                seen_messages: new_msg
            }
        case RESET_NOTIFICATION: 
            return {
                ...initialState.notification
            }
        case DELETE_OLD_NOTIFICATION:
                var new_msg = state.messages;
                new_msg = new_msg.filter(x => (!action.payload.includes(x.id)));
                return {
                    ...state,
                    messages: new_msg
                }    
       default: 
          return state;
    }
}  