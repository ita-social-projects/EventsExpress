import { GET_CONTACT_ADMIN_DATA } from '../../actions/contactAdmin/contact-admin-item-action';
import initialState from '../../store/initialState';

export const reducer = (state = initialState.contactAdminItem, action) => {
    switch (action.type) {
        case GET_CONTACT_ADMIN_DATA:
            return {
                ...state,
                data: action.payload
            }
        default:
            return state;
    }
}  
