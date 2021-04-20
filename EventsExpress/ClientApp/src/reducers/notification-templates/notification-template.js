import initialState from '../../store/initialState';
import { GET_TEMPLATE_SUCCESS } from '../../actions/notification-templates'

export const reducer = (state = initialState.notificationTemplate, action) => {
    switch (action.type) {
        case GET_TEMPLATE_SUCCESS:
            return {
                ...action.payload
            }
            
        default:
            return state;
    }
}
