
import initialState from '../store/initialState';
import {
    GET_CHAT_ERROR, GET_CHAT_PENDING, GET_CHAT_SUCCESS, INITIAL_CONNECTION, RECEIVE_MESSAGE
}from '../actions/chat';

export const reducer = (
    state = initialState.chat,
    action
  ) => {
    switch (action.type) {
      case INITIAL_CONNECTION:
        return {
            ...state,
            hubConnection: action.payload
        }
      case RECEIVE_MESSAGE:
          return {
            ...state,
            data: action.payload
          }
      case GET_CHAT_ERROR:
          return {
                ...state,
                isPending: false,
                isError: action.payload
            } 
    case GET_CHAT_PENDING:
            return {
                    ...state,
                    isPending: action.payload
                } 
      case GET_CHAT_SUCCESS:
          return {
              ...state,
              isPending: false,
              isSuccess: action.payload.isSuccess,
              data: action.payload.data
          }
       default: 
          return state;
    }
}  