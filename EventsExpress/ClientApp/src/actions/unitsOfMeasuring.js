import EventsExpressService from '../services/EventsExpressService';

export const GET_UNITS_OF_MEASURING = 'GET_UNITS_OF_MEASURING';

const api_serv = new EventsExpressService();

export default function get_unitsOfMeasuring() {
    return dispatch => {
        const res = api_serv.getUnitsOfMeasuring();
        res.then(response => {
            dispatch({
                type: GET_UNITS_OF_MEASURING,
                payload: response
            })
        });
    }
}