import initialState from '../store/initialState';
import { GET_CONFIGS_DATA } from '../actions/config/get-configs-action'

export const reducer = (state = initialState.configs, action) => {
    switch (action.type) {
        case GET_CONFIGS_DATA: return {
            ...state,
            keys: action.payload,
        };
        default: return state;
    }
}