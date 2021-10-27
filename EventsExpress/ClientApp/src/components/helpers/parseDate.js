import moment from 'moment';
export function parseDate(dateFrom) {
    let result = moment(dateFrom).format('ddd, MMM D LT [GMT]Z');
    return result.toUpperCase();
} 