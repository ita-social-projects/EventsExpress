import initialState from '../store/initialState';
import { SET_USER } from '../actions/login/login-action';
import { SET_LOGOUT } from '../actions/login/logout-action';
import { addUserCategory, addUserNotificationType, editBirthday, editGender, editUsername, changeAvatar } from '../actions/redactProfile/index';

export const reducer = (state = initialState.user, action) => {
    switch (action.type) {
        case SET_USER:
            return action.payload;

        case SET_LOGOUT:
            return initialState.user;

        case addUserCategory.UPDATE:
            return {
                ...state,
                categories: action.payload
            }
        case addUserNotificationType.UPDATE:
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
                name: action.payload.UserName
            }
        case editGender.UPDATE:
            return {
                ...state,
                gender: action.payload.Gender
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