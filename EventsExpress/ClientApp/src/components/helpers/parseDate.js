export function parseDate(dateFrom) {
    let date = new Date(dateFrom);
    let weekday = date.toLocaleString('en-us', { weekday: 'short' }).toUpperCase();
    let month = date.toLocaleString('en-us', { month: 'short' }).toUpperCase();
    let ddate = date.getDate();
    let hours = date.toLocaleString('en-us', { hours: 'short' })
    let ampm = hours.slice(hours.length - 2, hours.length)
    hours = hours.slice(12, hours.length - 6);

    let gmt = date.getTimezoneOffset();
    return weekday + ', ' + month + ' ' + ddate + ' @' + hours + ' ' + ampm + ' GMT' + gmt/60;
}