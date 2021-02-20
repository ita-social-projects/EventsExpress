import { authenticate } from '../actions/authentication-action';
import initialState from '../store/initialState';

export const reducer = (state = initialState.authenticate, action) => {
    switch (action.type) {
        case authenticate.PENDING:
            return { ...state, isPending: true }
        case authenticate.SUCCESS:
            return {
                ...state,
                isPending: false,
                isSucces: true
            }

        default:
            return state;
    }
}
