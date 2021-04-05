import initialState from '../../store/initialState';
import {
    SET_TRACKS_ERROR, SET_TRACKS_PENDING, GET_TRACKS_SUCCESS, SET_ENTITY_FILTER, GET_ENTITY_NAMES
} from '../../actions/tracks/track-list';

export const reducer = (state = initialState.tracks, action) => {
    switch (action.type) {
        case SET_TRACKS_ERROR:
            return {
                ...state,
                isPending: false,
                isError: action.payload
            }
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
                ...state.data.items.entityNames,
                entityNames: action.payload
            }
        case SET_ENTITY_FILTER:{
            return Object.assign({},state, {
                filter: Object.assign({}, state.filter, {
                    entityName: action.payload
                })
            })
        }
        default:
            return state;
    }
}  