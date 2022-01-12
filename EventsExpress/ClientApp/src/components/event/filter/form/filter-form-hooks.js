import { exclude, parse, stringify } from 'query-string';
import { useHistory } from 'react-router-dom';

const combineOrganizers = (ids, names) => {
    const organizers = [];
    for (let i = 0; i < ids.length; ++i) {
        organizers.push({
            id: ids[i],
            username: names[i]
        });
    }
    return organizers;
};

const applyFilters = (filters, history) => {
    filters.owners = filters?.organizers?.map(organizer => organizer.id);
    filters.ownersNames = filters?.organizers?.map(organizer => organizer.username);

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
    const organizersArePassedFromQuery = filter.owners && filter.owners.length !== 0;

    return {
        organizers: organizersArePassedFromQuery ? combineOrganizers(filter.owners, filter.ownersNames) : [],
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
