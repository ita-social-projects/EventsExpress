import {
    SET_ORGANIZERS,
    DELETE_ORGANIZER_FROM_SELECTED,
    SET_SELECTED_ORGANIZERS
} from '../../actions/events/filter/organizer-filter';

const initialState = {
    organizers: [],
    selectedOrganizers: []
};

export const organizerFilterReducer = (state = initialState, action) => {
    switch (action.type) {
        case SET_ORGANIZERS:
            return {
                ...state,
                organizers: action.payload
            };
        case SET_SELECTED_ORGANIZERS:
            return {
                ...state,
                selectedOrganizers: action.payload
            };
        case DELETE_ORGANIZER_FROM_SELECTED:
            return {
                ...state,
                selectedOrganizers: state.selectedOrganizers.filter(
                    organizer => organizer.id !== action.payload.id)
            };
        default:
            return state;
    }
};
