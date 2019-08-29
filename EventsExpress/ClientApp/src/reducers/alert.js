import{ _alert} from '../actions/alert';
import  initialState  from '../store/initialState';

export const reducer=(state = initialState.alert, action)=>{
    switch(action.type){
        case _alert.SET:
            return {
                variant: action.payload.variant,
                message:action.payload.message,
                autoHideDuration:action.payload.autoHideDuration,
                open:false
            };
            case _alert.SETOPEN:
                return {...state,
                    open: action.payload
                }
        case _alert.RESET:
            return initialState.alert;
        default:
            return state;
    };
}
