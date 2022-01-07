import { stringify, exclude } from 'query-string';
import { get_events } from '../../event/event-list-action';

export const applyFilters = filters => {
    return async dispatch => {
        filters.owners = filters?.organizers?.map(organizer => organizer.id);

        const options = { arrayFormat: 'index', skipNull: true };
        const filter = exclude(
            `?${stringify(filters, options)}`,
            ['organizers'],
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
