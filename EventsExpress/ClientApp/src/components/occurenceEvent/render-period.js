import 'moment-timezone';
import { enumPeriodicity }from '../../constants/PeriodicityConstants'

export function renderPeriod(periodicity, frequency) {

    if (periodicity === enumPeriodicity.Day && frequency > 1) {
        return `in ${frequency} days`;
    }
    if (periodicity === enumPeriodicity.Day && frequency === 1) {
        return "in a day";
    }
    if (periodicity === enumPeriodicity.Week && frequency > 1) {
        return `in ${frequency} weeks`;
    }
    if (periodicity === enumPeriodicity.Week && frequency === 1) {
        return "in a week";
    }
    if (periodicity === enumPeriodicity.Month && frequency > 1) {
        return `in ${frequency} months`;
    }
    if (periodicity === enumPeriodicity.Month && frequency === 1) {
        return "in a month";
    }
    if (periodicity === enumPeriodicity.Year && frequency > 1) {
        return `in ${frequency} years`;
    }
    if (periodicity === enumPeriodicity.Year && frequency === 1) {
        return "in a year";
    }
}

