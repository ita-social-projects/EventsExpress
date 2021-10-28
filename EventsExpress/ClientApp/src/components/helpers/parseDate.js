import moment from 'moment';
export function parseDate(date) {
    let result = moment(date).format('ddd, MMM D LT [GMT]Z');
    return result.toUpperCase();
} 