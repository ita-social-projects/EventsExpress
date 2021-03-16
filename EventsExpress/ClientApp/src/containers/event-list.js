import React, { Component } from 'react';
import { connect } from 'react-redux';
import { parse as queryStringParse } from 'query-string';
import EventList from '../components/event/event-list';
import Spinner from '../components/spinner';
import { get_events } from '../actions/event/event-list-action';
import history from '../history';
import eventHelper from '../components/helpers/eventHelper';

class EventListWrapper extends Component {
    constructor(props) {
        super(props);
        this.objCurrentQueryParams = Object.create(null);
    }

    componentDidMount() {
        this.setSearchParamsToEventFilter(this.props.location.search);
        this.executeSearchEvents();
    }

    componentDidUpdate(prevProps) {
        const objFilterParams = eventHelper.trimUndefinedKeys(this.props.events.filter);
        if (this.hasUpdateSearchParams(objFilterParams)) {
            this.objCurrentQueryParams = objFilterParams;
            this.executeSearchEvents();
        }
    }

    hasUpdateSearchParams = objFilterParams => {
        return !eventHelper.compareObjects(objFilterParams, this.objCurrentQueryParams);
    }

    executeSearchEvents = () => {
        const queryString = eventHelper.getQueryStringByEventFilter(this.props.events.filter);
        this.props.get_events(queryString);
        history.push(`${this.props.location.pathname}${queryString}`);
    }

    setSearchParamsToEventFilter = search => {
        this.objCurrentQueryParams = queryStringParse(search);

        Object.entries(this.objCurrentQueryParams).forEach(function ([key, value]) {
            this.props.events.filter[key] = value;
        }.bind(this));

        this.objCurrentQueryParams = eventHelper.trimUndefinedKeys(this.props.events.filter);
    }

    render() {
        let current_user = this.props.current_user.id !== null
            ? this.props.current_user
            : {};
        const { data, isPending } = this.props.events;
        const { items } = this.props.events.data;
        const spinner = isPending ? <Spinner /> : null;
        const content = !isPending
            ? <EventList
                current_user={current_user}
                data_list={items}
                filter={this.props.events.filter}
                page={data.pageViewModel.pageNumber}
                totalPages={data.pageViewModel.totalPages}
            />
            : null;
        return <>
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
    }
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(EventListWrapper);
