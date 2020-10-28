import React, { Component } from 'react';
import { Redirect } from 'react-router';
import { connect } from 'react-redux';
import * as queryString from 'query-string';
import EventList from '../components/event/event-list';
import Spinner from '../components/spinner';
import { get_events, get_eventsForAdmin } from '../actions/event-list';
import BadRequest from '../components/Route guard/400';
import Unauthorized from '../components/Route guard/401';
import Forbidden from '../components/Route guard/403';
import history from '../history';

class EventListWrapper extends Component {
    constructor() {
        super();
        let queryParams = Object.create(null);
    }

    componentDidUpdate(prevProps) {
        this.queryParams = queryString.parse(window.location.search);

        if (this.hasUpdateSearchParams()) {
            this.executeSearchEvents();
        }
    }

    componentDidMount() {
        this.queryParams = queryString.parse(this.props.location.search);
        const queryKeys = Object.keys(this.queryParams);

        if (queryKeys.length > 0) {
            queryKeys.forEach(function (key) {
                this.props.events.searchParams[key] = this.queryParams[key];
            }.bind(this));
        }

        this.executeSearchEvents();
    }

    hasUpdateSearchParams = function () {
        let searchParams = queryString.parse(
            queryString.stringify(this.props.events.searchParams));
        let keysSearchParams = Object.keys(searchParams);
        let keysQueryParams = Object.keys(this.queryParams);

        if (keysSearchParams.length !== keysQueryParams.length) {
            return true;
        }

        for (const key of keysSearchParams) {
            if (searchParams[key] !== this.queryParams[key]) {
                return true;
            }
        }

        return false;
    }

    executeSearchEvents = () => {
        if (this.props.current_user.role == "Admin") {
            this.props.get_eventsForAdmin(this.props.events.searchParams);
        } else {
            this.props.get_events(this.props.events.searchParams);
        }

        history.push(queryString.stringifyUrl({
            url: this.props.location.pathname,
            query: this.props.events.searchParams
        }));
    }

    render() {
        let current_user = this.props.current_user.id != null ? this.props.current_user : {};
        const { data, isPending, isError } = this.props.events;
        const { items } = this.props.events.data;
        const errorMessage = isError.ErrorCode == '403'
            ? <Forbidden />
            : isError.ErrorCode == '500'
                ? <Redirect
                    from="*"
                    to={{
                        pathname: "/home/events",
                        search: `?${queryString.stringify(this.props.events.searchParams)}`,
                    }}
                />
                : isError.ErrorCode == '401'
                    ? <Unauthorized />
                    : isError.ErrorCode == '400'
                        ? <BadRequest />
                        : null;
        const spinner = isPending ? <Spinner /> : null;
        const content = !errorMessage
            ? <EventList
                current_user={current_user}
                data_list={items}
                page={data.pageViewModel.pageNumber}
                totalPages={data.pageViewModel.totalPages}
                callback={this.getEvents}
            />
            : null;

        return <>
            {errorMessage}
            {spinner || content}
        </>
    }
}

const mapStateToProps = (state) => {
    return {
        events: state.events,
        current_user: state.user
    }
};

const mapDispatchToProps = (dispatch) => {
    return {
        get_events: (filter) => dispatch(get_events(filter)),
        get_eventsForAdmin: (filter) => dispatch(get_eventsForAdmin(filter)),
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(EventListWrapper);
