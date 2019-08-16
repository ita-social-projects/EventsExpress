import EventsExpressService from '../services/EventsExpressService';
import * as SignalR from '@aspnet/signalr';

export const GET_CHAT_PENDING = "GET_CHAT_PENDING";
export const GET_CHAT_SUCCESS = "GET_CHAT_SUCCESS";
export const GET_CHAT_ERROR = "GET_CHAT_ERROR";
export const INITIAL_CONNECTION = "INITIAL_CONNECTION";
export const RECEIVE_MESSAGE = "RECEIVE_MESSAGE";

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

export function initialConnection(token) {
    const hubConnection = new SignalR.HubConnectionBuilder().withUrl(`${window.location.origin}/chatroom`,
            { accessTokenFactory: () => (localStorage.getItem('token')) } ).build();

    hubConnection
          .start()
          .then(() => console.log('Connection started!'))
          .catch(err => console.log('Error while establishing connection :('));

    hubConnection.on('ReceiveMessage', (data) => ({
        type: RECEIVE_MESSAGE,
        payload: data
    }));

    return {
        type: INITIAL_CONNECTION,
        payload: hubConnection
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
