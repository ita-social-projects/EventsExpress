
import initialState from '../store/initialState';
import {
     RECEIVE_MESSAGE, DELETE_OLD_NOTIFICATION
}from '../actions/chat';

import {
    GET_UNREAD_MESSAGES, RESET_NOTIFICATION
}from '../actions/chats';


export const reducer = (
    state = initialState.notification,
    action
  ) => {
    switch (action.type) {
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