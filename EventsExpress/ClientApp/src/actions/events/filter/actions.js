import { stringify, exclude } from 'query-string';
import { get_events } from '../../event/event-list-action';
import { enumLocationType } from '../../../constants/EventLocationType';

export const applyFilters = filters => {
    return async dispatch => {
        filters.owners = filters?.organizers?.map(organizer => organizer.id);
        filters.locationtype = filters?.location.type;
        if(filters.location.type === enumLocationType.map)
        {
            filters.x = filters?.location.latitude;
            filters.y = filters?.location.longitude;
            filters.radius = filters?.location.radius;
        }
        const options = { arrayFormat: 'index', skipNull: true };
        const filter = exclude(
            `?${stringify(filters, options)}`,
            ['organizers'],
            ['location'],
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
