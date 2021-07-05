import { CHANGE_STATUS } from '../../actions/contactAdmin/contact-admin-issue-status-action';
import initialState from '../../store/initialState';

export const reducer = (state = initialState.contactAdminList, action) => {
    switch (action.type) {
        case CHANGE_STATUS:
            return {
                ...state,
                data: action.payload,
            }
        default:
            return state;
    }
}
