import { exclude, parse, stringify } from 'query-string';
import { useHistory } from 'react-router-dom';

const applyFilters = (filters, history) => {
    filters.owners = filters?.organizers;

    const options = { arrayFormat: 'index', skipNull: true };
    const filter = exclude(
        `?${stringify(filters, options)}`,
        ['organizers'],
        options
    );

    history.push(`${history.location.pathname}${filter}`);
};

const resetFilters = history => {
    history.push(history.location.pathname);
};

const parseFilters = query => {
    const filter = parse(query, { arrayFormat: 'index' });

    return {
        organizers: [],
        ...filter
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
