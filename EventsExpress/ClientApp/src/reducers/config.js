import initialState from '../store/initialState';
import { GET_CONFIGS_PENDING, GET_CONFIGS_SUCCESS } from '../actions/config/get-configs-action'

export const reducer = (state = initialState.configs, action) => {
    switch (action.type) {
        case GET_CONFIGS_PENDING: return {
            ...state,
            isConfigsPending: false
        };
        case GET_CONFIGS_SUCCESS: return{
            keys:action.payload,
                isConfigsSuccess : false,
                    
        };
        default: return state;
    }
}