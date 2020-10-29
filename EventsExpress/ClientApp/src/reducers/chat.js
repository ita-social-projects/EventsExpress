import initialState from '../store/initialState';
import {
    GET_CHAT_ERROR, GET_CHAT_PENDING, GET_CHAT_SUCCESS, RESET_CHAT, CONCAT_NEW_MSG, RECEIVE_SEEN_MESSAGE
} from '../actions/chat';

export const reducer = (
    state = initialState.chat,
    action
) => {
    let new_msg = state.data.messages;

    switch (action.type) {
        case RESET_CHAT:
            return {
                ...initialState.chat
            }
        case CONCAT_NEW_MSG:
            new_msg = new_msg.concat(action.payload);
            return {
                ...state,
                data: {
                    ...state.data,
                    messages: new_msg
                }
            }
        case RECEIVE_SEEN_MESSAGE:
            new_msg = state.data.messages;
            new_msg = new_msg.map(x => {
                if (action.payload.includes(x.id)) {
                    x.seen = true;
                }
                return x;
            });
            return {
                ...state,
                data: {
                    ...state.data,
                    messages: new_msg
                }
            }
        case GET_CHAT_ERROR:
            return {
                ...state,
                isPending: false,
                isError: action.payload
            }
        case GET_CHAT_PENDING:
            return {
                ...state,
                isPending: action.payload
            }
        case GET_CHAT_SUCCESS:
            return {
                ...state,
                isPending: false,
                isSuccess: action.payload.isSuccess,
                data: action.payload.data
            }
        default:
            return state;
    }
}
