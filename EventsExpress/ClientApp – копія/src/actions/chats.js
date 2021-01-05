import { ChatService } from '../services';

export const GET_CHATS_PENDING = "GET_CHATS_PENDING";
export const GET_CHATS_SUCCESS = "GET_CHATS_SUCCESS";
export const GET_CHATS_ERROR = "GET_CHATS_ERROR";
export const GET_UNREAD_MESSAGES = "GET_UNREAD_MESSAGES";
export const RESET_NOTIFICATION = "RESET_NOTIFICATION";

const api_serv = new ChatService();

export default function get_chats() {

    return dispatch => {
        dispatch(getChatsPending(true));
        const res = api_serv.getChats();
        res.then(response => {
            if (response.error == null) {
                dispatch(getChatsSuccess({ isSuccess: true, data: response }));
            } else {
                dispatch(getChatsError(response.error));
            }
        });
    }
}

export function resetNotification() {
    return dispatch =>
        dispatch({ type: RESET_NOTIFICATION });
}

export function getUnreadMessages(userId) {
    return dispatch => {
        var res = api_serv.getUnreadMessages(userId);
        res.then(response => {
            if (response.error == null) {
                dispatch({ type: GET_UNREAD_MESSAGES, payload: response });
            }
        });
    }
}

export function getChatsSuccess(data) {
    return {
        type: GET_CHATS_SUCCESS,
        payload: data
    };
}

export function getChatsPending(data) {
    return {
        type: GET_CHATS_PENDING,
        payload: data
    };
}

export function getChatsError(data) {
    return {
        type: GET_CHATS_ERROR,
        payload: data
    };
}
