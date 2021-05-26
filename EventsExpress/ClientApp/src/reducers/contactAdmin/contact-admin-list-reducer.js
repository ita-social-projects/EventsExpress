import {
    GET_CONTACT_ADMIN_SUCCESS,
    SET_CONTACT_ADMIN_PENDING,
    RESET_CONTACT_ADMIN
} from '../../actions/contactAdmin/contact-admin-list-action';
import initialState from '../../store/initialState';

export const reducer = (state = initialState.contactAdminList, action) => {
    switch (action.type) {
        case SET_CONTACT_ADMIN_PENDING:
            return {
                ...state,
                isPending: action.payload,
            }
        case GET_CONTACT_ADMIN_SUCCESS:
            return {
                ...state,
                isPending: false,
                data: action.payload
            }
        case RESET_CONTACT_ADMIN:
            return initialState.contactAdminList;
        default:
            return state;
    }
}