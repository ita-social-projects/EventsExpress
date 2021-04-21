import initialState from '../store/initialState';
import { getRoles } from '../actions/roles';

export const reducer = (state = initialState.roles, action) => {
    switch(action.type) {
        case getRoles.PENDING:
            return {
                ...state,
                isPending: true,                
            }

        case getRoles.SUCCESS:
            return{
                ...state,
                isPending: false,
                data: action.payload
            }

        default:
            return state;
    }
}