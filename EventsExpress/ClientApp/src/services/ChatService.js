import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class ChatService {

    getChat = async (chatId) => {
        const res = await baseService.getResource(`chat/${chatId}`);
        return res;
    }

    getChats = async () => {
        const res = await baseService.getResource('chat/All');
        return res;
    }

    getUnreadMessages = async (userId) => {
        const res = await baseService.getResource(`chat/GetUnreadMessage/?userId=${userId}`);
        return res;
    }
}