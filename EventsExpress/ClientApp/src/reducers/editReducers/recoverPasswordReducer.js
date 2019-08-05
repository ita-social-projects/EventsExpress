import { recover_Password, recoverPassword } from '../../actions/EditProfile/recoverPassword';

export const reducer = (
    state = {
        isRecoverPasswordPending: false,
        isRecoverPasswordSuccess: false,
        RecoverPasswordError: null
    },
    action) => {
    switch (action.type) {
        case recoverPassword.PENDING:
            return Object.assign({}, state, {
                isRecoverPasswordPending: action.isRecoverPasswordPending
            });

        case recoverPassword.SUCCESS:
            return Object.assign({}, state, {
                isRecoverPasswordSuccess: action.isRecoverPasswordSuccess
            });

        case recoverPassword.ERROR:
            return Object.assign({}, state, {
                RecoverPasswordError: action.RecoverPasswordError
            });

        default:
            return state;
    }
}