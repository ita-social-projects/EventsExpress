import { editBirthday } from '../../actions/redactProfile/birthday-edit-action';

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

        default:
            return state;
    }
}