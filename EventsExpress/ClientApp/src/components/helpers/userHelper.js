export function getQueryStringByUsersFilter (filter) {
    return `?keyWord=${filter !== undefined 
        ? filter.keyWord
        : ''}`;
}