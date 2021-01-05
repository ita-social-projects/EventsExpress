
import initialState from '../store/initialState';
import {
    GET_CITY_ERROR, GET_CITY_PENDING, GET_CITY_SUCCESS
}from '../actions/cities';

export const reducer = (
    state = initialState.cities,
    action
  ) => {
    switch (action.type) {
      case GET_CITY_ERROR:
          return {
                ...state,
                isPending: false,
                isError: action.payload
            } 
    case GET_CITY_PENDING:
            return {
                    ...state,
                    isPending: action.payload
                } 
      case GET_CITY_SUCCESS:
          return {
              ...state,
              isPending: false,
              data: action.payload
          }
       default: 
          return state;
    }
}  