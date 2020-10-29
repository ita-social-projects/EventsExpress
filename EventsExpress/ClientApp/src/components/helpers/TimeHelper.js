import * as moment from 'moment';

export function getTimeDifferenceFromNull(value) {
    return moment.utc(value).fromNow();
}