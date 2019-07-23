import { editBirthday } from '../../actions/EditProfile/editBirthday';

export const reducer = (
    state = {
        isEditBirthdayPending: false,
        isEditBirthdaySuccess: false,
        EditUsernameError: null
    },
    action) => {
    switch (action.type) {
        case editBirthday.PENDING:
            return Object.assign({}, state, {
                isEditBirthdayPending: action.isEditBirthdayPending
            });

        case editBirthday.SUCCESS:
            return Object.assign({}, state, {
                isEditBirthdaySuccess: action.isEditBirthdaySuccess
            });

        case editBirthday.ERROR:
            return Object.assign({}, state, {
                EditBirthdayError: action.EditBirthdayError
            });

        default:
            return state;
    }
}