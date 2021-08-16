import initialState from '../../store/initialState';
import { GET_TEMPLATE_PROPERTIES_SUCCESS, GET_TEMPLATE_SUCCESS } from '../../actions/notification-templates'

export const reducer = (state = initialState.notificationTemplate, action) => {
    if (action.type === GET_TEMPLATE_SUCCESS) {
        return {
            ...state,
            ...action.payload
        }
    } else if(action.type === GET_TEMPLATE_PROPERTIES_SUCCESS) {
        return {
            ...state,
            necessaryProperties: action.payload
        }
    }

    return state;
}
