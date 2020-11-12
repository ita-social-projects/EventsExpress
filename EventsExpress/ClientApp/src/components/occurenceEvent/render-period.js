import 'moment-timezone';

export function renderPeriod(periodicity, frequency) {

    if (periodicity === 0 && frequency > 1) {
        return `in ${frequency} days`
    }
    if (periodicity === 0 && frequency === 1) {
        return "in a day"
    }
    if (periodicity === 1 && frequency > 1) {
        return `in ${frequency} weeks`
    }
    if (periodicity === 1 && frequency === 1) {
        return "in a week"
    }
    if (periodicity === 2 && frequency > 1) {
        return `in ${frequency} months`
    }
    if (periodicity === 2 && frequency === 1) {
        return "in a month"
    }
    if (periodicity === 3 && frequency > 1) {
        return `in ${frequency} years`
    }
    if (periodicity === 3 && frequency === 1) {
        return "in a year"
    }
}

