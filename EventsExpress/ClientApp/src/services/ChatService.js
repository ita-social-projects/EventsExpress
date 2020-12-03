import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class ChatService {

    getChat = async (chatId) => {
        const res = await baseService.getResource(`chat/${chatId}/GetChat`);
        return res;
    }

    getChats = async () => {
        const res = await baseService.getResource('chat/GetAllChats');
        return res;
    }

    getUnreadMessages = async (userId) => {
        const res = await baseService.getResource(`chat/${userId}/GetUnreadMessages`);
        return res;
    }
}