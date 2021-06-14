import { recoverPassword } from '../../actions/redactProfile/password-recover-action';
import initialState from './../../store/initialState'

export const reducer = (state = initialState.recoverPassword, action) => {
    switch (action.type) {
        case recoverPassword.PENDING:
            return { ...state, isPending: true }           
        case recoverPassword.DATA:
            return {
                ...state,
                isError: false
            }
        default:
            return state;
    }
}

