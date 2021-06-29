import initialState from '../../store/initialState';
import {
    GET_TRACKS_DATA, GET_ENTITY_NAMES, RESET_TRACKS
} from '../../actions/tracks/track-list-action';

export const reducer = (state = initialState.tracks, action ) => { 
    switch (action.type) {
        case GET_TRACKS_DATA:
            return {
                ...state,
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