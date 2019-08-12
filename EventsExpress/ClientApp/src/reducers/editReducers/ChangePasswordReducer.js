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
            return {
                ...state,
                isChangePasswordPending: action.isChangePasswordPending
            };

        case changePassword.SUCCESS:
            return {
                ...state,
                isChangePasswordSuccess: action.isChangePasswordSuccess,
                ChangePasswordError: null
            };

        case changePassword.ERROR:
            return {
                ...state, 
                ChangePasswordError: action.ChangePasswordError,
                isChangePasswordSuccess:false
            };

        default:
            return state;
    }
}