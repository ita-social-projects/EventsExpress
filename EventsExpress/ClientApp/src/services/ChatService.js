import EventsExpressService from './EventsExpressService'

const baseService = new EventsExpressService();

export default class ChatService {

    getChat = chatId => baseService.getResource(`chat/${chatId}`);

    getChats = () => baseService.getResource('chat/All');

    getUnreadMessages = userId =>
        baseService.getResource(`chat/GetUnreadMessages/?userId=${userId}`);
}