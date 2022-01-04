import { stringify } from 'query-string';
import { get_events } from '../../event/event-list-action';

export const applyFilters = filters => {
    return async dispatch => {
        const filter = stringify({
            ...filters,
            organizers: undefined,
            owners: filters.organizers.map(organizer => organizer.id)
        }, { arrayFormat: 'index', skipNull: true });
        dispatch(get_events(`?${filter}`));
    }
}
