import { contactAdmin } from '../../actions/contactAdmin/contact-admin-add-action';
import initialState from '../../store/initialState';

export const reducer = (state = initialState.contactAdmin, action) => {
    switch (action.type) {
        case contactAdmin.DATA:
            return {
                ...state,
                data: action.payload,
            }
        default:
            return state;
    }
}