import initialState from '../../store/initialState';
import { GET_USER_NOTIFICATION_TYPES_DATA } from '../../actions/notificationType/userNotificationType-action';

export const reducer = (state = initialState.user, action) => {
    switch (action.type) {
        case GET_USER_NOTIFICATION_TYPES_DATA:
            return {
                ...state,
                notificationTypes: action.payload
            }
        default:
            return state;
    }
}