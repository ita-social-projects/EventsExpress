
import initialState from '../store/initialState';
import {
    GET_EVENT_ERROR,GET_EVENT_PENDING,GET_EVENT_SUCCESS
}from '../actions/event-item-view';

export const reducer = (
    state = initialState.event,
    action
  ) => {
    switch (action.type) {
      case GET_EVENT_ERROR:
          return {
                ...state,
                isPending: false,
                isError: action.payload
            } 
    case GET_EVENT_PENDING:
            return {
                    ...state,
                    isPending: action.payload
                } 
      case GET_EVENT_SUCCESS:
          return {
              ...state,
              isPending: false,
              data: action.payload
          }
       default: 
          return state;
    }
}  