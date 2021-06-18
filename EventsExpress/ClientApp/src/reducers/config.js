import initialState from '../store/initialState';
import { GET_CONFIG_PENDING, GET_CONFIG_SUCCESS } from '../actions/config/get-configs-action'

export const reducer = (state = initialState.config, action) => {
    switch (action.type) {
        case GET_CONFIG_PENDING: return {
            ...state,
            isConfigsPending: false
        };
        case GET_CONFIG_SUCCESS: return {
            ...state,
            ...action.payload,
            isConfigsSuccess: true,
        };
        default: return state;
    }
}
