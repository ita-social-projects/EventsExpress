import { changePassword } from '../../actions/editProfile/password-change-action';

export const reducer = (
    state = {
        isChangePasswordPending: false,
        isChangePasswordSuccess: false,
    },
    action) => {
    switch (action.type) {
        case changePassword.PENDING:
            return {
                ...state,
                isChangePasswordPending: action.isChangePasswordPending
            };
        case changePassword.SUCCESS:
            return {
                ...state,
                isChangePasswordPending: false,
                isChangePasswordSuccess: false,
                ChangePasswordError: null
            };
        default:
            return state;
    }
}