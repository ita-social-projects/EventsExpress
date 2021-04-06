
import initialState from '../store/initialState';
import {
    SET_COMMENTS_PENDING, GET_COMMENTS_SUCCESS
} from '../actions/comment/comment-list-action';

export const reducer = (
    state = initialState.comments,
    action
) => {
    switch (action.type) {
        case SET_COMMENTS_PENDING:
            return {
                ...state,
                isPending: action.payload
            }
        case GET_COMMENTS_SUCCESS:
            return {
                ...state,
                isPending: false,
                data: action.payload
            }
        default:
            return state;
    }
}  