import EventsExpressService from '../services/EventsExpressService';
import * as SignalR from '@aspnet/signalr';
import { connect } from 'react-redux';


export const GET_CHAT_PENDING = "GET_CHAT_PENDING";
export const GET_CHAT_SUCCESS = "GET_CHAT_SUCCESS";
export const GET_CHAT_ERROR = "GET_CHAT_ERROR";
export const INITIAL_CONNECTION = "INITIAL_CONNECTION";
export const RECEIVE_MESSAGE = "RECEIVE_MESSAGE";
export const RECEIVE_NOTIFICATION = "RECEIVE_NOTIFICATION";
export const RESET_HUB = "RESET_HUB";
export const RESET_CHAT = "RESET_CHAT";
export const RECEIVE_SEEN_MESSAGE = "RECEIVE_SEEN_MESSAGE";
export const CONCAT_NEW_MSG = "CONCAT_NEW_MSG";
export const DELETE_OLD_NOTIFICATION = "DELETE_OLD_NOTIFICATION";
export const DELETE_SEEN_MSG_NOTIFICATION = "DELETE_SEEN_MSG_NOTIFICATION";

const api_serv = new EventsExpressService();

export default function get_chat(chatId) {

    return dispatch => {
        dispatch(getChatPending(true));

        const res = api_serv.getChat(chatId);
        res.then(response => {
            if (response.error == null) {
                dispatch(getChatSuccess({ isSuccess: true, data: response }));
            } else {
                dispatch(getChatError(response.error));
            }
        });
    }
}
export function initialConnection(props) {
    return dispatch => {
        const hubConnection = new SignalR.HubConnectionBuilder().withUrl(`${window.location.origin}/chatroom`,
            { accessTokenFactory: () => (localStorage.getItem('token')) }).build();

        hubConnection
            .start()
            .then(() =>{ hubConnection.on('ReceiveMessage', (data) => {
                    dispatch(ReceiveMsg(data));
            });
            hubConnection.on('wasSeen', (data) => {
                dispatch(ReceiveSeenMsg(data));
        });
        }
            )
            .catch(err => console.log('Error while establishing connection :('));

        dispatch({
            type: INITIAL_CONNECTION,
            payload: hubConnection
        });
    }
}
export function deleteSeenMsgNotification(id){
    return dispatch => dispatch({
        type: DELETE_SEEN_MSG_NOTIFICATION,
        payload: id
    });
}
export function deleteOldNotififcation(data){
    return dispatch => dispatch({
        type: DELETE_OLD_NOTIFICATION,
        payload: data
    });
}
export function concatNewMsg(data){
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

export function getChatError(data) {
    return {
        type: GET_CHAT_ERROR,
        payload: data
    };
}
