import { GET_CONTACTUS_SUCCESS, SET_CONTACTUS_PENDING } from '../../actions/contactUs/contact-us-item-action';
import initialState from '../../store/initialState';

export const reducer = (state = initialState.contactUsItem, action) => {
    switch (action.type) {
        case SET_CONTACTUS_PENDING:
            return {
                ...state,
                isPending: action.payload,
            }
        case GET_CONTACTUS_SUCCESS:
            return {
                ...state,
                isPending: false,
                data: action.payload
            }
        default:
            return state;
    }
}  
