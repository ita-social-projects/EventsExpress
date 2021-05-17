import {
    GET_CONTACTUS_SUCCESS,
    SET_CONTACTUS_PENDING,
    RESET_CONTACTUS
} from '../../actions/contactUs/contact-us-list-action';
import initialState from '../../store/initialState';

export const reducer = (state = initialState.contactUsList, action) => {
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
        case RESET_CONTACTUS:
            return initialState.contactUsList;
        default:
            return state;
    }
}