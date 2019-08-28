import{_dialog} from '../actions/dialog';
import initialState from '../store/initialState';


export const reducer=(state=initialState.dialog, action)=>{
    switch(action.type){
        case _dialog.SET:
            return{
                title: action.payload.title,
                message: action.payload.message,
                callback: action.payload.action,
                open:true
            };
            
        case _dialog.SETOPEN:
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