import { ChatService } from '../../services';
import * as SignalR from '@aspnet/signalr';
import { setErrorAllertFromResponse, setAlert } from '../alert-action';

export const GET_CHAT_PENDING = "GET_CHAT_PENDING";
export const GET_CHAT_SUCCESS = "GET_CHAT_SUCCESS";
export const INITIAL_CONNECTION = "INITIAL_CONNECTION";
export const RECEIVE_MESSAGE = "RECEIVE_MESSAGE";
export const RECEIVE_NOTIFICATION = "RECEIVE_NOTIFICATION";
export const RESET_HUB = "RESET_HUB";
export const RESET_CHAT = "RESET_CHAT";
export const RECEIVE_SEEN_MESSAGE = "RECEIVE_SEEN_MESSAGE";
export const CONCAT_NEW_MSG = "CONCAT_NEW_MSG";
export const DELETE_OLD_NOTIFICATION = "DELETE_OLD_NOTIFICATION";
export const DELETE_SEEN_MSG_NOTIFICATION = "DELETE_SEEN_MSG_NOTIFICATION";
export const RECEIVED_NEW_EVENT = "RECEIVED_NEW_EVENT";

const api_serv = new ChatService();

export default function get_chat(chatId) {

    return async dispatch => {
        dispatch(getChatPending(true));

        let response = await api_serv.getChat(chatId);
        if (!response.ok) {
            dispatch(setErrorAllertFromResponse(response));
            return Promise.reject();
        }
        let jsonRes = await response.json();
        dispatch(getChatSuccess({ isSuccess: true, data: jsonRes }));
        return Promise.resolve();
    }
}

export function initialConnection() {
    return async dispatch => {
        const hubConnection = new SignalR.HubConnectionBuilder().withUrl(`${window.location.origin}/chatroom`,
            { accessTokenFactory: () => (localStorage.getItem('token')) }).build();
        try {
            await hubConnection.start();

            hubConnection.on('ReceiveMessage', (data) => {
                dispatch(ReceiveMsg(data));
                if (data.senderId != localStorage.getItem('id')) {
                    dispatch(setAlert({ variant: 'info', message: "You have a new message", autoHideDuration: 5000 }));
                }
            });
            hubConnection.on('wasSeen', (data) => {
                dispatch(ReceiveSeenMsg(data));
            });

            hubConnection.on('ReceivedNewEvent', (data) => {
                dispatch(receivedNewEvent(data));
                dispatch(setAlert({
                    variant: 'info',
                    message: `The event was created which could interested you.`,
                    autoHideDuration: 5000
                }));
            });
        }
        catch { (err => console.log('Error while establishing connection :(')); }

        dispatch({
            type: INITIAL_CONNECTION,
            payload: hubConnection
        });
    }
}

function receivedNewEvent(data) {
    return {
        type: RECEIVED_NEW_EVENT,
        payload: data
    }
}

export function deleteSeenMsgNotification(id) {
    return dispatch => dispatch({
        type: DELETE_SEEN_MSG_NOTIFICATION,
        payload: id
    });
}
export function deleteOldNotififcation(data) {
    return dispatch => dispatch({
        type: DELETE_OLD_NOTIFICATION,
        payload: data
    });
}
export function concatNewMsg(data) {
    return dispatch => dispatch({
        type: CONCAT_NEW_MSG,
        payload: data
    });
}

export function ReceiveSeenMsg(data) {
    return {
        type: RECEIVE_SEEN_MESSAGE,
        payload: data
    }
}

export function ReceiveMsg(data) {
    return {
        type: RECEIVE_MESSAGE,
        payload: data
    }
}

export function reset_hub() {
    return {
        type: RESET_CHAT,
        payload: {}
    }
}

export function reset() {
    return {
        type: RESET_CHAT,
        payload: {}
    }
}

export function getChatSuccess(data) {
    return {
        type: GET_CHAT_SUCCESS,
        payload: data
    };
}

export function getChatPending(data) {
    return {
        type: GET_CHAT_PENDING,
        payload: data
    };
}

