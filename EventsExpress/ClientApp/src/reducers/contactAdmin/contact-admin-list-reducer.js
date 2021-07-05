import {
    GET_CONTACT_ADMIN_DATA,
    RESET_CONTACT_ADMIN
} from '../../actions/contactAdmin/contact-admin-list-action';
import initialState from '../../store/initialState';

export const reducer = (state = initialState.contactAdminList, action) => {
    switch (action.type) {
        case GET_CONTACT_ADMIN_DATA:
            return {
                ...state,
                data: action.payload
            }
        case RESET_CONTACT_ADMIN:
            return initialState.contactAdminList;
        default:
            return state;
    }
}