import { exclude, parse, stringify } from 'query-string';
import { useHistory } from 'react-router-dom';
import { enumLocationType } from '../../../constants/EventLocationType';
import { DISPLAY_USER_EVENTS } from '../../../constants/user-to-event-relation';

const applyFilters = (filters, history) => {
    if (filters.location)
    {
        filters.locationtype = filters.location.type;
        if (filters.location.type === enumLocationType.map) {
            filters.x = filters.location.latitude;
            filters.y = filters.location.longitude;
            filters.radius = filters.location.radius;
        }
    }

    const options = { arrayFormat: 'index', skipNull: true };
    const filter = exclude(
        `?${stringify(filters, options)}`,
        ['location'],
        options
    );

    history.push(`${history.location.pathname}${filter}`);
};

const appendFilters = (filters, history) => {
    const currentFilters = parse(history.location.search);
    applyFilters({ ...currentFilters, ...filters }, history);
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
};

const parseFilters = query => {
    const filters = parse(query, {
        arrayFormat: 'index',
        parseNumbers: true,
        parseBooleans: true
    });

    return {
        categories: [],
        organizers: [],
        onlyAdult: false,
        withChildren: false,
        location: filters.locationtype !== undefined ? parseLocation(filters) : { type: null },
        ...filters
    };
};

const getQueryWithRequestFilters = query => {
    const filters = parse(query, {
        arrayFormat: 'index',
        parseNumbers: true,
        parseBooleans: true
    });

    filters.displayUserEvents = sessionStorage.getItem(DISPLAY_USER_EVENTS);

    filters.isOnlyForAdults = (filters.onlyAdult !== filters.withChildren)
        ? (filters.onlyAdult ?? false)
        : null;

    const options = { arrayFormat: 'index', skipNull: true };
    return exclude(
        `?${stringify(filters, options)}`,
        ['onlyAdult', 'withChildren'],
        options
    );
};

export const useFilterInitialValues = () => {
    const history = useHistory();
    return parseFilters(history.location.search);
};

export const useFilterActions = () => {
    const history = useHistory();
    return {
        applyFilters: filters => applyFilters(filters, history),
        appendFilters: filters => appendFilters(filters, history),
        resetFilters: () => resetFilters(history),
        getQueryWithRequestFilters: () => getQueryWithRequestFilters(history.location.search),
    };
};
