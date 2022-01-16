import { exclude, parse, stringify } from 'query-string';
import { useHistory } from 'react-router-dom';
import { enumLocationType } from '../../../../constants/EventLocationType';

const applyFilters = (filters, history) => {
    filters.owners = filters?.organizers;
    filters.isOnlyForAdults =
        (filters.onlyAdult !== filters.withChildren)
            ? (filters.onlyAdult ?? false)
            : null;
    filters.locationtype = filters?.location.type;
    if (filters.location.type === enumLocationType.map)
    {
        filters.x = filters?.location.latitude;
        filters.y = filters?.location.longitude;
        filters.radius = filters?.location.radius;
    }

    const options = { arrayFormat: 'index', skipNull: true };
    const filter = exclude(
        `?${stringify(filters, options)}`,
        ['organizers', 'location', 'withChildren', 'onlyAdult'],
        options
    );

    history.push(`${history.location.pathname}${filter}`);
};

const resetFilters = history => {
    history.push(history.location.pathname);
};

const parseLocation = filters => {
    const location = { type: filters.locationtype };
    delete filters.locationtype;

    if (location.type === enumLocationType.map) {
        location.latitude = filters.x;
        location.longitude = filters.y;
        location.radius = filters.radius;

        delete filters.x;
        delete filters.y;
        delete filters.radius;
    }

    return location;
}

const transformAgeRestriction = filters => {
    if (filters.isOnlyForAdults !== undefined) {
        filters.onlyAdult = filters.isOnlyForAdults;
        filters.withChildren = !filters.isOnlyForAdults
        delete filters.isOnlyForAdults;
    }
}

const parseFilters = query => {
    const filters = parse(query, { arrayFormat: 'index', parseNumbers: true, parseBooleans: true });
    transformAgeRestriction(filters);

    return {
        organizers: filters.owners || [],
        onlyAdult: false,
        withChildren: false,
        location: filters.locationtype !== undefined ? parseLocation(filters) : { type: null },
        ...filters
    };
};

export const useFilterFormInitialValues = () => {
    const history = useHistory();
    return parseFilters(history.location.search);
};

export const useFilterFormActions = () => {
    const history = useHistory();
    return {
        applyFilters: filters => applyFilters(filters, history),
        resetFilters: () => resetFilters(history)
    };
};
