import initialState from '../store/initialState';
import { SET_USER } from '../actions/login/login-action';
import { SET_LOGOUT } from '../actions/login/logout-action';
import { addUserCategory } from '../actions/editProfile/userCategory-add-action';
import { addUserNotificationType } from '../actions/editProfile/userNotificationType-add-action';
import { editBirthday } from '../actions/editProfile/birthday-edit-action';
import { editGender } from '../actions/editProfile/gender-edit-action';
import { editUsername } from '../actions/editProfile/userName-edit-action';
import { changeAvatar } from '../actions/editProfile/avatar-change-action';
import { authenticate } from '../actions/authentication-action';

export const reducer = (state = initialState.user, action) => {
    switch (action.type) {
        case SET_USER:
            return action.payload;

        case authenticate.SET_AUTHENTICATE:
            localStorage.setItem("token", action.payload.token);
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