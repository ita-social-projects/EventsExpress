import initialState from '../../store/initialState';
import {
    SET_TRACKS_PENDING, GET_TRACKS_SUCCESS, GET_ENTITY_NAMES, RESET_TRACKS
} from '../../actions/tracks/track-list-action';

export const reducer = (state = initialState.tracks, action ) => { 
    switch (action.type) {
        case SET_TRACKS_PENDING:
            return {
                ...state,
                isPending: action.payload
            }
        case GET_TRACKS_SUCCESS:
            return {
                ...state,
                isPending: false,
                data: action.payload
            }
        case GET_ENTITY_NAMES:
            return {
                ...state,
                entityNames: action.payload
            }
        case RESET_TRACKS:
            return initialState.tracks;
        default:
            return state;
    }
}  