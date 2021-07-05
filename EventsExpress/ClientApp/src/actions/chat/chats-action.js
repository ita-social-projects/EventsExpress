import { ChatService } from '../../services';
import { setErrorAllertFromResponse } from '../alert-action';
import { getRequestInc, getRequestDec } from "../request-count-action";

export const GET_CHATS_DATA = "GET_CHATS_DATA";
export const GET_UNREAD_MESSAGES = "GET_UNREAD_MESSAGES";
export const RESET_NOTIFICATION = "RESET_NOTIFICATION";

const api_serv = new ChatService();

export default function get_chats() {

    return async dispatch => {
        dispatch(getRequestInc());
        let response = await api_serv.getChats();
        dispatch(getRequestDec());
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getChatsSuccess(jsonRes));
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
            let jsonRes = await response.json();
            dispatch({ type: GET_UNREAD_MESSAGES, payload: jsonRes });
            return Promise.resolve();
        }
    }
}

export function getChatsSuccess(data) {
    return {
        type: GET_CHATS_DATA,
        payload: data
    };
}