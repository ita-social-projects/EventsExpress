import EventsExpressService from '../services/EventsExpressService';



export const GET_CHATS_PENDING = "GET_CHATS_PENDING";
export const GET_CHATS_SUCCESS = "GET_CHATS_SUCCESS";
export const GET_CHATS_ERROR = "GET_CHATS_ERROR";

const api_serv = new EventsExpressService();

export default function get_chats() {

    return dispatch => {
        dispatch(getChatsPending(true));

        const res = api_serv.getChats();
        console.log(res);
        res.then(response => {
            console.log(response);
            if (response.error == null) {

                dispatch(getChatsSuccess({isSuccess: true, data: response}));
            } else {
                dispatch(getChatsError(response.error));
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
