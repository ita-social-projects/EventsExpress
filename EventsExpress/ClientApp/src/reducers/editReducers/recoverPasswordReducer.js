import { recoverPassword } from '../../actions/redactProfile/password-recover-action';
import initialState from './../../store/initialState'

export const reducer = (state = initialState.recoverPassword, action) => {
    switch (action.type) {
        case recoverPassword.PENDING:
            return { ...state, isPending: true }           
        case recoverPassword.SUCCESS:
            return {
                ...state,
                isPending: false,
                isSucces: true,
                isError: false
            }
        default:
            return state;
    }
}

