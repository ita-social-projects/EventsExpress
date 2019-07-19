
import initialState from '../store/initialState';
import {
    SET_EVENTS_ERROR,SET_EVENTS_PENDING,GET_EVENTS_SUCCESS
}from '../actions/event-list';

export const reducer = (
    state = initialState.events,
    action
  ) => {
    switch (action.type) {
      case SET_EVENTS_ERROR:
          return {
                ...state,
                isPending: false,
                isError: action.payload
            } 
    case SET_EVENTS_PENDING:
            return {
                    ...state,
                    isPending: action.payload
                } 
      case GET_EVENTS_SUCCESS:
          return {
              ...state,
              isPending: false,
              data: action.payload
          }
       default: 
          return state;
    }
}  