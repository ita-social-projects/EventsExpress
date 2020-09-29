
import initialState from '../store/initialState';
import {
    GET_CHATS_ERROR, GET_CHATS_PENDING, GET_CHATS_SUCCESS
} from '../actions/chats';
import { RECEIVE_MESSAGE } from '../actions/chat';

export const reducer = (
    state = initialState.chats,
    action
) => {
    switch (action.type) {
        case RECEIVE_MESSAGE:
            var chat = state.data.find(x => x.id == action.payload.chatRoomId);
            var newChats = state.data.filter(x => x.id != action.payload.chatRoomId);
            if (chat != null) {
                chat.lastMessage = action.payload.text;
                chat.lastMessageTime = action.payload.dateCreated;
                newChats = newChats.concat(chat);
            }
            return {
                ...state,
                data: newChats
            }
        case GET_CHATS_ERROR:
            return {
                ...state,
                isPending: false,
                isError: action.payload
            }
        case GET_CHATS_PENDING:
            return {
                ...state,
                isPending: action.payload
            }
        case GET_CHATS_SUCCESS:
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
