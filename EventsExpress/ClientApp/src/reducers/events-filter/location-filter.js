import { SET_LOCATION } from '../../actions/events/filter/location-filter';

const initialState = {
    location:{}
};

export const locationFilterReducer = (state = initialState, action) => {
    switch (action.type) {
        case SET_LOCATION:
            return {
                ...state,
                location: action.payload
            };
        default:
            return state;
    }
};
