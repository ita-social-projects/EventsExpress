import { ChatService } from '../../services';
import { setErrorAllertFromResponse } from '../alert-action';

export const GET_CHATS_PENDING = "GET_CHATS_PENDING";
export const GET_CHATS_SUCCESS = "GET_CHATS_SUCCESS";
export const GET_UNREAD_MESSAGES = "GET_UNREAD_MESSAGES";
export const RESET_NOTIFICATION = "RESET_NOTIFICATION";

const api_serv = new ChatService();

export default function get_chats() {

    return async dispatch => {
        dispatch(getChatsPending(true));
        let response = await api_serv.getChats();
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getChatsSuccess({ isSuccess: true, data: jsonRes }));
        return Promise.resolve();
    }
}

export function resetNotification() {
    return dispatch =>
        dispatch({ type: RESET_NOTIFICATION });
}

export function getUnreadMessages(userId) {
    return async dispatch => {
        let response = await api_serv.getUnreadMessages(userId);
        if (response.ok) {
            dispatch({ type: GET_UNREAD_MESSAGES, payload: response });
            return Promise.resolve();
        }
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

