import{authenticate} from'../actions/authentication'
import initialState from '../store/initialState';

export const reducer=(
    state=initialState.authenticate,
    action
   )=>{
       switch(action.type){
           case authenticate.PENDING:
               return{...state, isPending: true}
           case authenticate.SUCCESS:
                return {
                    ...state,
                    isPending: false,
                    isSucces: true
                }
            case authenticate.ERROR:
                return {
                    ...state,
                    isPending: false,
                    isError: action.payload
                }

            default:
                return state;         
       }
   }