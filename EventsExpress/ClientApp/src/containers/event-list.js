import React, { useState, useEffect } from 'react';
import { connect } from 'react-redux';
import { withRouter } from "react-router";
import EventList from '../components/event/event-list';
import SpinnerWrapper from './spinner';
import { get_events } from '../actions/event/event-list-action';
import { useFilterActions } from '../components/event/filter/filter-hooks';

const EventListWrapper = ({ history, events, data, currentUser, getEvents }) => {
    const [prevQuery, setPrevQuery] = useState(null);

    const { getQueryWithRequestFilters } = useFilterActions();

    useEffect(() => {
        const query = history.location.search;
        if (query !== prevQuery) {
            const filterQuery = getQueryWithRequestFilters();
            getEvents(filterQuery);
            setPrevQuery(query);
        }
    });

    return (
        <SpinnerWrapper showContent={data !== undefined}>
            <EventList
                current_user={currentUser.id !== null ? currentUser : {}}
                data_list={data.items}
                filter={events.filter}
                page={data.pageViewModel.pageNumber}
                totalPages={data.pageViewModel.totalPages}
                customNoResultsMessage="No events meet the specified criteria. Please make another choice."
            />
        </SpinnerWrapper>
    );
};

const mapStateToProps = state => ({
    events: state.events,
    data: state.events.data,
    currentUser: state.user,
});

const mapDispatchToProps = dispatch => ({
    getEvents: query => dispatch(get_events(query)),
});

export default withRouter(connect(
    mapStateToProps,
    mapDispatchToProps
)(EventListWrapper));
