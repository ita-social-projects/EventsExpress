import initialState from '../store/initialState';
import { SET_USER } from '../actions/login/login-action';
import { SET_LOGOUT } from '../actions/login/logout-action';
import { addUserCategory, addUserNotificationType, editBirthday, editGender, editUsername, changeAvatar,editLocation, editFirstname,editLastname } from '../actions/redactProfile';
import { GET_USER_NOTIFICATION_TYPES_DATA } from '../actions/notificationType/userNotificationType-action';
import { GET_USER_CATEGORIES_DATA } from '../actions/category/userCategory-action';

export const reducer = (state = initialState.user, action) => {
    switch (action.type) {
        case SET_USER:
            return action.payload;

        case SET_LOGOUT:
            return initialState.user;

        case addUserCategory.UPDATE:
        case GET_USER_CATEGORIES_DATA:
            return {
                ...state,
                categories: action.payload
            }
        case addUserNotificationType.UPDATE:
        case GET_USER_NOTIFICATION_TYPES_DATA:
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
        case editFirstname.UPDATE:
            return {
                ...state,
                firstName: action.payload.firstName
            }
        case editLastname.UPDATE:
            return {
                ...state,
                lastName: action.payload.lastName
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
        case editLocation.UPDATE:
            return{
                ...state,
                location : action.payload
            }
        default:
            return state;
    }
}