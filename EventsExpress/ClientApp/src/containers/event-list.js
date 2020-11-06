import React, { Component } from 'react';
import { Redirect } from 'react-router';
import { connect } from 'react-redux';
import { parse as queryStringParse } from 'query-string';
import EventList from '../components/event/event-list';
import Spinner from '../components/spinner';
import { get_events } from '../actions/event-list';
import BadRequest from '../components/Route guard/400';
import Unauthorized from '../components/Route guard/401';
import Forbidden from '../components/Route guard/403';
import history from '../history';
import eventHelper from '../components/helpers/eventHelper';

class EventListWrapper extends Component {
    constructor(props) {
        super(props);
        this.objQueryParams = Object.create(null);
    }

    componentDidUpdate(prevProps) {
        if (this.hasUpdateSearchParams()) {
            this.objQueryParams = eventHelper.trimUndefinedKeys(this.props.events.filter);
            this.executeSearchEvents();
        }
    }

    componentDidMount() {
        this.objQueryParams = queryStringParse(this.props.location.search);

        Object.entries(this.objQueryParams).forEach(function ([key, value]) {
            this.props.events.filter[key] = value;
        }.bind(this));

        this.executeSearchEvents();
    }

    hasUpdateSearchParams = function () {
        const objFilterParams = eventHelper.trimUndefinedKeys(this.props.events.filter);
        return !eventHelper.compareObjects(objFilterParams, this.objQueryParams);
    }

    executeSearchEvents = () => {
        const queryString = eventHelper.getQueryStringByEventFilter(this.props.events.filter);
        this.props.get_events(queryString);
        history.push(`${this.props.location.pathname}${queryString}`);
    }

    render() {
        let current_user = this.props.current_user.id !== null
            ? this.props.current_user
            : {};
        const { data, isPending, isError } = this.props.events;
        const { items } = this.props.events.data;
        const errorMessage = isError.ErrorCode == '403'
            ? <Forbidden />
            : isError.ErrorCode == '500'
                ? <Redirect
                    from="*"
                    to={{
                        pathname: "/home/events",
                        search: eventHelper.getQueryStringByEventFilter(this.props.events.filter),
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
                filter={this.props.events.filter}
                page={data.pageViewModel.pageNumber}
                totalPages={data.pageViewModel.totalPages}
            />
            : null;

        return <>
            {!errorMessage
                ? spinner || content
                : errorMessage
            }
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
    }
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(EventListWrapper);
