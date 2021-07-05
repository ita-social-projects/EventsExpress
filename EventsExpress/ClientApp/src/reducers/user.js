import initialState from '../store/initialState';
import { SET_USER } from '../actions/login/login-action';
import { SET_LOGOUT } from '../actions/login/logout-action';
import { addUserCategory, addUserNotificationType, editBirthday, editGender, editUsername, changeAvatar } from '../actions/redactProfile/index';
import { GET_USER_NOTIFICATION_TYPES_DATA } from '../actions/notificationType/userNotificationType-action';
import { GET_USER_CATEGORIES_DATA } from '../actions/category/userCategory-action';

export const reducer = (state = initialState.user, action) => {
    switch (action.type) {
        case SET_USER:
            return action.payload;

        case SET_LOGOUT:
            return initialState.user;

        case addUserCategory.UPDATE && GET_USER_CATEGORIES_DATA:
            return {
                ...state,
                categories: action.payload
            }
        case addUserNotificationType.UPDATE && GET_USER_NOTIFICATION_TYPES_DATA:
            return {
                 ...state,
                notificationTypes: action.payload
            }
        case editBirthday.UPDATE:
            return {
                ...state,
                birthday: new Date(action.payload).toDateString()
            }
        case editUsername.UPDATE:
            return {
                ...state,
                name: action.payload.userName
            }
        case editGender.UPDATE:
            return {
                ...state,
                gender: action.payload.gender
            }
        case changeAvatar.UPDATE:
            return {
                ...state,
                photoUrl: action.payload
            }
        default:
            return state;
    }
}