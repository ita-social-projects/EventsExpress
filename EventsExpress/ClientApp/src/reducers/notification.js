
import initialState from '../store/initialState';
import {
    GET_CHAT_ERROR, GET_CHAT_PENDING, GET_CHAT_SUCCESS, INITIAL_CONNECTION, RECEIVE_MESSAGE, RECEIVE_SEEN_MESSAGE, DELETE_OLD_NOTIFICATION
}from '../actions/chat';

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