import { CHANGE_STATUS, SET_CONTACT_ADMIN_PENDING } from '../../actions/contactAdmin/contact-admin-issue-status-action';
import initialState from '../../store/initialState';

export const reducer = (state = initialState.contactAdminList, action) => {
    switch (action.type) {
        case CHANGE_STATUS:
            let stateChangeStatus = { ...state };
            return {
                stateChangeStatus,
                isPending: action.payload,
            }
        case SET_CONTACT_ADMIN_PENDING:
            return {
                ...state,
                isPending: true,
                data: action.payload
            }
        default:
            return state;
    }
}
