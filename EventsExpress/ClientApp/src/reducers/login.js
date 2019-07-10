import { requestTOKEN, receiveTOKEN, receiveERROR } from '../constants/index'


const initialState = { isReqested: false, isAuthorized: false, token: "", error: "" };

export const reducer = (state = initialState, action) => {

    if (action.type === requestTOKEN) {
        return {
            ...state,
            isReqested: true
        };
    }

    if (action.type === receiveTOKEN) {
        return {
            ...state,
            isAuthorized: true,
            token: action.payload
        };
    }

    if (action.type === receiveERROR) {
        return {
            ...state,
            error: action.payload
        };
    }

    return state;
};