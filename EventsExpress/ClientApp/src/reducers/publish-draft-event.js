import initialState from '../store/initialState';

import {
    PUBLISH_EVENT, 
} from '../actions/event/event-add-action';

export const reducer = (state = initialState.publishErrors, action) => {

    if (action.type === PUBLISH_EVENT) {
        return {
            ...state,
            data:action.payload
        };
    }
    return state;
};



    

