import initialState from '../store/initialState';
import {SET_USER} from '../actions/login';
import {SET_LOGOUT} from '../actions/logout';

export const reducer = (state, action) => {
    state = state || initialState.user;
    switch(action.type)
        {
            case SET_USER:
                return action.payload;

            case SET_LOGOUT:
                return initialState.user;
        }
    return state;
}