import{ _alert} from '../actions/alert';
import  initialState  from '../store/initialState';

export const reducer=(state = initialState.alert, action)=>{
    switch(action.type){
        case _alert.SET:
            return {
                variant: action.payload.variant,
                className: action.payload.className,
                message:action.payload.message,
                open:true
            };
    
        case _alert.RESET:
            return {
                variant:null,
                className:null,
                message:null,
                open:false
            }
        default:
            return state;
    };
}
