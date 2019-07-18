import {
    SET_EDITBIRTHDAY_PENDING,
    SET_EDITBIRTHDAY_SUCCESS,
    SET_EDITBIRTHDAY_ERROR
} from '../../actions/EditProfile/editBirthday';

export const reducer = (
    state = {
        isEditBirthdayPending: false,
        isEditBirthdaySuccess: false,
        EditUsernameError: null
    },
    action) => {
    switch (action.type) {
        case SET_EDITBIRTHDAY_PENDING:
            return Object.assign({}, state, {
                isEditBirthdayPending: action.isEditBirthdayPending
            });

        case SET_EDITBIRTHDAY_SUCCESS:
            return Object.assign({}, state, {
                isEditBirthdaySuccess: action.isEditBirthdaySuccess
            });

        case SET_EDITBIRTHDAY_ERROR:
            return Object.assign({}, state, {
                EditBirthdayError: action.EditBirthdayError
            });

        default:
            return state;
    }
}