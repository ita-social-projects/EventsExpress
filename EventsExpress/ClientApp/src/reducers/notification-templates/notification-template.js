import initialState from '../../store/initialState';
import { GET_TEMPLATE_SUCCESS, SET_TEMPLATE_SUCCESS } from '../../actions/notification-templates'

export const reducer = (state = initialState.notificationTemplate, action) => {
    switch (action.type) {
        case SET_TEMPLATE_SUCCESS:
        case GET_TEMPLATE_SUCCESS:
            return {
                ...action.payload
            }
            
        default:
            return state;
    }
};