
import initialState from '../store/initialState';
import {
    SET_COUNTRY_ERROR, SET_COUNTRY_PENDING, SET_COUNTRY_SUCCESS
}from '../actions/countries';

export const reducer = (
    state = initialState.countries,
    action
  ) => {
    switch (action.type) {
      case SET_COUNTRY_ERROR:
          return {
                ...state,
                isPending: false,
                isError: action.payload
            } 
    case SET_COUNTRY_PENDING:
            return {
                    ...state,
                    isPending: action.payload
                } 
      case SET_COUNTRY_SUCCESS:
          return {
              ...state,
              isPending: false,
              data: action.payload
          }
       default: 
          return state;
    }
}  