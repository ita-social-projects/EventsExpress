import { stringify, exclude } from 'query-string';
import { get_events } from '../../event/event-list-action';

export const applyFilters = filters => {
    return async dispatch => {
        filters.owners = filters?.organizers?.map(organizer => organizer.id);
        filters.isOnlyForAdults =
            (filters.onlyAdult !== filters.withChildren)
            ? (filters.onlyAdult ?? false)
            : null;

        const options = { arrayFormat: 'index', skipNull: true };
        const filter = exclude(
            `?${stringify(filters, options)}`,
            ['organizers', 'withChildren', 'onlyAdult'],
            options
        );

        dispatch(get_events(filter));
    };
};

export const resetFilters = () => {
    return async dispatch => {
        dispatch(get_events(''));
    };
};
