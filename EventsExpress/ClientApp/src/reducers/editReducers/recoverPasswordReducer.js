import { recoverPassword } from '../../actions/EditProfile/recoverPassword';
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

        case recoverPassword.ERROR:
            return {
                ...state,
                isPending: false,
                isError: action.payload
            }

        default:
            return state;
    }
}

/*
         isPending: false,
        isError: false,
        isSucces:false,
 */