import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class ChatService {

    getChat = chatId => baseService.getResourceNew(`chat/${chatId}`);

    getChats = () => baseService.getResourceNew('chat/All');

    getUnreadMessages = userId =>
        baseService.getResource(`chat/GetUnreadMessages/?userId=${userId}`);
}