import{contactUs} from '../actions/contact-us';
import initialState from '../store/initialState';


export const reducer=(state=initialState.contactUs, action)=>{
    switch(action.type){
        case contactUs.PENDING:
            return{...state, isPending:true}
        case contactUs.SUCCESS:
            return{
                ...state,
                isPending: false,
                isSucces: true,
                isError: false
            }
        case contactUs.ERROR:
            return{
                ...state,
                isPending: false,
                isError: action.payload
            }    
        default:
            return state; 
    }
}