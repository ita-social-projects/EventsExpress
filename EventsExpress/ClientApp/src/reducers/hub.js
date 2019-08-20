
import initialState from '../store/initialState';
import {
    GET_CHAT_ERROR, GET_CHAT_PENDING, GET_CHAT_SUCCESS, INITIAL_CONNECTION, RECEIVE_MESSAGE, RESET_HUB
}from '../actions/chat';

export const reducer = (
    state = initialState.hubConnection,
    action
  ) => {
    switch (action.type) {
      case INITIAL_CONNECTION:
        return action.payload
      case RESET_HUB:
          return null
       default: 
          return state;
    }
}  