import { GET_CONTACT_ADMIN_SUCCESS, SET_CONTACT_ADMIN_PENDING } from '../../actions/contactAdmin/contact-admin-item-action';
import initialState from '../../store/initialState';

export const reducer = (state = initialState.contactAdminItem, action) => {
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
        default:
            return state;
    }
}  
