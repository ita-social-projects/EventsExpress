
import initialState from '../store/initialState';
import { GET_COMMENTS_DATA } from '../actions/comment/comment-list-action';

export const reducer = (
    state = initialState.comments,
    action
) => {
    switch (action.type) {
        case GET_COMMENTS_DATA:
            return {
                ...state,
                data: action.payload
            }
        default:
            return state;
    }
}  