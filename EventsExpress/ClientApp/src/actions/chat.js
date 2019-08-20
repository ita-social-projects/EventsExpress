import EventsExpressService from '../services/EventsExpressService';
import * as SignalR from '@aspnet/signalr';

import { bindActionCreators } from 'redux';
export const GET_CHAT_PENDING = "GET_CHAT_PENDING";
export const GET_CHAT_SUCCESS = "GET_CHAT_SUCCESS";
export const GET_CHAT_ERROR = "GET_CHAT_ERROR";
export const INITIAL_CONNECTION = "INITIAL_CONNECTION";
export const RECEIVE_MESSAGE = "RECEIVE_MESSAGE";
export const RESET_HUB = "RESET_HUB";
export const RESET_CHAT = "RESET_CHAT";

const api_serv = new EventsExpressService();

export default function get_chat(chatId) {

    return dispatch => {
        dispatch(getChatPending(true));

        const res = api_serv.getChat(chatId);
        res.then(response => {
            if (response.error == null) {
                dispatch(getChatSuccess({isSuccess: true, data: response}));
            } else {
                dispatch(getChatError(response.error));
            }
        });
    }
}

export function initialConnection() {
    return dispatch => {
    const hubConnection = new SignalR.HubConnectionBuilder().withUrl(`${window.location.origin}/chatroom`,
            { accessTokenFactory: () => (localStorage.getItem('token')) } ).build();

    hubConnection
          .start()
          .then (() => hubConnection.on('ReceiveMessage', (data) => {
              console.log('receive');
                           dispatch(ReceiveMsg(data));
                                                                    }))
          .catch(err => console.log('Error while establishing connection :('));

    dispatch({
        type: INITIAL_CONNECTION,
        payload: hubConnection
    });
}
}

export function ReceiveMsg(data){
    return  {
        type: RECEIVE_MESSAGE,
        payload: data
        }
}

export function reset_hub(){
    return {
        type: RESET_CHAT,
        payload: {}
    }
}

export function reset(){
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
