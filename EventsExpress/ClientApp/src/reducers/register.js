import { SET_REGISTER_PENDING, SET_REGISTER_SUCCESS, SET_REGISTER_ERROR} from '../actions/register';
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
        console.log('wsdsf');
        return Object.assign({}, state, {
            isRegisterSuccess: action.payload,
            registerError: null,
            isRegisterPending: false,
        });
  
      case SET_REGISTER_ERROR:
        return Object.assign({}, state, {
            registerError: action.registerError,
            isRegisterSuccess: false,
            isRegisterPending: false
        });
  
      default:
        return state;
    }
  }