import initialState from '../store/initialState';
import { getRoles } from '../actions/roles';

export const reducer = (state = initialState.roles, action) => {
    switch(action.type) {
        case getRoles.DATA:
            return{
                ...state,
                data: action.payload
            }

        default:
            return state;
    }
}