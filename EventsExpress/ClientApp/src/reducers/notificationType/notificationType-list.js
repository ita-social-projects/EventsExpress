
import initialState from '../../store/initialState';
import { GET_NOTIFICATION_TYPES_DATA } from '../../actions/notificationType/notificationType-list-action';

export const reducer = (state = initialState.notificationTypes, action) => {
    switch (action.type) {
        case GET_NOTIFICATION_TYPES_DATA:
            return {
                ...state,
                data: action.payload
            }
        default:
            return state;
    }
}