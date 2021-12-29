import { SET_ORGANIZERS } from '../../actions/events/filter/organizer-filter';

const initialState = {
    organizers: []
};

export const organizerFilterReducer = (state = initialState, action) => {
    switch (action.type) {
        case SET_ORGANIZERS:
            return {
                ...state,
                organizers: action.payload
            };
        default:
            return state;
    }
};
