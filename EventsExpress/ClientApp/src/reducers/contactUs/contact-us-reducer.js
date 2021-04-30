import { contactUs } from '../../actions/contactUs/contact-us-add-action';
import initialState from '../../store/initialState';

export const reducer = (state = initialState.contactUs, action) => {
    switch (action.type) {
        case contactUs.PENDING:
            return { ...state, isPending: true, data: action.payload }
        case contactUs.SUCCESS:
            return {
                ...state,
                isPending: false,
                isSucces: true,
                data: action.payload,
            }
        default:
            return state;
    }
}