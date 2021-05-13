import { CHANGE_STATUS, SET_CONTACTUS_PENDING } from '../../actions/contactUs/contact-us-issue-status-action';
import initialState from '../../store/initialState';

export const reducer = (state = initialState.contactUsList, action) => {
    switch (action.type) {
        case CHANGE_STATUS:
            let stateChangeStatus = { ...state };
            return stateChangeStatus;
        case SET_CONTACTUS_PENDING:
            return {
                ...state,
                isPending: true,
                data: action.payload
            }
        default:
            return state;
    }
}
