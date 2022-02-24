import React, { useState, useEffect } from 'react';
import { connect } from 'react-redux';
import { withRouter } from "react-router";
import EventList from '../components/event/event-list';
import SpinnerWrapper from './spinner';
import { get_events } from '../actions/event/event-list-action';
import { useFilterActions } from '../components/event/filter/filter-hooks';
import { CarouselLayout } from '../components/layouts/carousel-layout';
import { ListLayout } from '../components/layouts/list-layout';
import { EventCarouselCard } from '../components/event/layouts/carousel/event-carousel-card';
import { EventListCard } from '../components/event/layouts/list/event-list-card';

const EventListWrapper = ({ history, events, data, currentUser, getEvents }) => {
    const [prevQuery, setPrevQuery] = useState(null);

    const { getQueryWithRequestFilters, appendFilters } = useFilterActions();

    useEffect(() => {
        const query = history.location.search;
        if (query !== prevQuery) {
            const filterQuery = getQueryWithRequestFilters();
            getEvents(filterQuery);
            setPrevQuery(query);
        }
    });

    const nextPage = () => {
        appendFilters({ page: data.pageViewModel.pageNumber + 1 });
    };

    const previousPage = () => {
        appendFilters({ page: data.pageViewModel.pageNumber - 1 });
    };

    const layouts = {
        list: () => (
            <ListLayout
                onNext={nextPage}
                onPrevious={previousPage}
                hasNext={data.pageViewModel.hasNextPage}
                hasPrevious={data.pageViewModel.hasPreviousPage}
            >
                {data.items.map(event => (
                    <EventListCard key={event.id} event={event} />
                ))}
            </ListLayout>
        ),
        matrix: () => (
            <EventList
                current_user={currentUser.id !== null ? currentUser : {}}
                data_list={data.items}
                filter={events.filter}
                page={data.pageViewModel.pageNumber}
                totalPages={data.pageViewModel.totalPages}
                customNoResultsMessage="No events meet the specified criteria. Please make another choice."
            />
        ),
        carousel: () => (
            <CarouselLayout
                onNext={nextPage}
                onPrevious={previousPage}
                hasNext={data.pageViewModel.hasNextPage}
                hasPrevious={data.pageViewModel.hasPreviousPage}
            >
                {data.items.map(event => (
                    <EventCarouselCard key={event.id} event={event} />
                ))}
            </CarouselLayout>
        )
    };

    return (
        <SpinnerWrapper showContent={data !== undefined}>
            {layouts[events.layout ?? 'list']()}
        </SpinnerWrapper>
    );
};

const mapStateToProps = state => ({
    events: state.events,
    data: state.events.data,
    currentUser: state.user
});

const mapDispatchToProps = dispatch => ({
    getEvents: query => dispatch(get_events(query))
});

export default withRouter(connect(
    mapStateToProps,
    mapDispatchToProps
)(EventListWrapper));
