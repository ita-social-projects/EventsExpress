
import initialState from '../../store/initialState';
import {
    SET_NOTIFICATION_TYPES_PENDING, GET_NOTIFICATION_TYPES_SUCCESS
} from '../../actions/notificationType/notificationType-list-action';

export const reducer = (state = initialState.notificationTypes, action) => {
    switch (action.type) {
        case SET_NOTIFICATION_TYPES_PENDING:
            return {
                ...state,
                isPending: action.payload
            }
        case GET_NOTIFICATION_TYPES_SUCCESS:
            return {
                ...state,
                isPending: false,
                data: action.payload
            }
        default:
            return state;
    }
}