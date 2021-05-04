import { SET_REGISTER_PENDING, SET_REGISTER_SUCCESS} from '../actions/register/register-action';
import initialState from '../store/initialState';
export const reducer = (
    state = initialState.register,
    action
  ) => {
    switch (action.type) {
      case SET_REGISTER_PENDING:
        return Object.assign({}, state, {
            isRegisterPending: action.isRegisterPending
        });
  
      case SET_REGISTER_SUCCESS:
        return Object.assign({}, state, {
            isRegisterSuccess: action.payload,
            registerError: null,
            isRegisterPending: false,
        });
  
      default:
        return state;
    }
}

