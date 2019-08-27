import{_dialog} from '../actions/dialog';
import initialState from '../store/initialState';
import { _alert } from '../actions/alert';

export const reducer=(state=initialState.dialog, action)=>{
    switch(action.type){
        case _dialog.SET:
            return{
                title:action.payload.title,
                message:action.payload.message,
                open:false
            };
            
        case _alert.SETOPEN:
            return {
                ...state,
                open:action.payload
            }
           
        case _dialog.RESET:
            return initialState.dialog;

        default:
            return state
    };
}