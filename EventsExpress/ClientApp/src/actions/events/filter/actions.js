import { stringify, exclude, parse } from 'query-string';

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

export const applyFilters = (filters, history) => {
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

export const resetFilters = history => {
    history.push(history.location.pathname);
};

export const parseFilters = query => {
    const filter = parse(query, { arrayFormat: 'index' });
    const organizersArePassedFromQuery = filter.owners && filter.owners.length !== 0;

    return {
        organizers: organizersArePassedFromQuery ? combineOrganizers(filter.owners, filter.ownersNames) : [],
        ...filter
    };
};
