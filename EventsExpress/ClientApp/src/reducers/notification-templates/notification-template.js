import initialState from '../../store/initialState';
import { GET_TEMPLATE_SUCCESS } from '../../actions/notification-templates'

export const reducer = (state = initialState.notificationTemplate, action) => {
    if (action.type === GET_TEMPLATE_SUCCESS) {
        return {
            ...action.payload
        }
    }

    return state;
}
