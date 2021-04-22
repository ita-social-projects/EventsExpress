import { contactUs } from '../actions/contact-us-action';
import initialState from '../store/initialState';

export const reducer = (state = initialState.contactUs, action) => {
    switch (action.type) {
        case contactUs.PENDING:
            return { ...state, isPending: true }
        case contactUs.SUCCESS:
            return {
                ...state,
                isPending: false,
                isSucces: true,
            }
        default:
            return state;
    }
}