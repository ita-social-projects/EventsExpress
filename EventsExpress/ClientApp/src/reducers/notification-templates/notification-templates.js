import initialState from '../../store/initialState';
import { GET_TEMPLATES_SUCCESS } from '../../actions/notification-templates'

export const reducer = (state = initialState.notificationTemplates, action) => {
    switch (action.type) {
        case GET_TEMPLATES_SUCCESS:
            return {
                data: action.payload
            }
            
        default:
            return state;
    }
}
