import { changePassword } from '../../actions/EditProfile/changePassword';

export const reducer = (
    state = {
        isChangePasswordPending: false,
        isChangePasswordSuccess: false,
        ChangePasswordError: null
    },
    action) => {
    switch (action.type) {
        case changePassword.PENDING:
            return Object.assign({}, state, {
                isChangePasswordPending: action.isChangePasswordPending
            });

        case changePassword.SUCCESS:
            return Object.assign({}, state, {
                isChangePasswordSuccess: action.isChangePasswordSuccess
            });

        case changePassword.ERROR:
            return Object.assign({}, state, {
                ChangePasswordError: action.ChangePasswordError
            });

        default:
            return state;
    }
}