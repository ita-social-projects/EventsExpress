import EventsExpressService from '../services/EventsExpressService';

const api_serv = new EventsExpressService();

export default function get_unitofmeasuring() {
    return api_serv.getUnitsOfMeasuring();
}