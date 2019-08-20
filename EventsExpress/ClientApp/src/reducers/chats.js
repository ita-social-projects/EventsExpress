
import initialState from '../store/initialState';
import {
    GET_CHATS_ERROR, GET_CHATS_PENDING, GET_CHATS_SUCCESS
}from '../actions/chats';

export const reducer = (
    state = initialState.chats,
    action
  ) => {
    switch (action.type) {
      case GET_CHATS_ERROR:
          return {
                ...state,
                isPending: false,
                isError: action.payload
            } 
    case GET_CHATS_PENDING:
            return {
                    ...state,
                    isPending: action.payload
                } 
      case GET_CHATS_SUCCESS:
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