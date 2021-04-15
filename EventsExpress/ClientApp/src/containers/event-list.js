import React, { Component } from 'react';
import { getFormValues } from 'redux-form';
import { connect } from 'react-redux';
import { parse as queryStringParse } from 'query-string';
import EventList from '../components/event/event-list';
import Spinner from '../components/spinner';
import { get_events } from '../actions/event/event-list-action';
import eventHelper from '../components/helpers/eventHelper';
import { withRouter } from "react-router";

class EventListWrapper extends Component {
    constructor(props) {
        super(props);
        this.objCurrentQueryParams = Object.create(null);
        this.prevQueryStringSearch = "";
    }

    componentDidMount() {
        this.setSearchParamsToEventFilter(this.props.history.location.search);
        const queryString = eventHelper.getQueryStringByEventFilter(this.objCurrentQueryParams);
        this.props.get_events(queryString);
    }

    componentDidUpdate() {
        if (window.location.search != this.prevQueryStringSearch) {
            this.prevQueryStringSearch = window.location.search;
            this.props.get_events(window.location.search);
        }
    }

    setSearchParamsToEventFilter = search => {
        var filterCopy = { ...this.props.events.filter };
        this.objCurrentQueryParams = queryStringParse(search);

        Object.entries(this.objCurrentQueryParams).forEach(function ([key, value]) {
            filterCopy[key] = value;
        }.bind(this));
        this.objCurrentQueryParams = eventHelper.trimUndefinedKeys(filterCopy);
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
        current_user: state.user,
        form_values: getFormValues('event-filter-form')(state),
    }
};

const mapDispatchToProps = (dispatch) => {
    return {
        get_events: (filter) => dispatch(get_events(filter)),
    }
};

export default withRouter(connect(
    mapStateToProps,
    mapDispatchToProps
)(EventListWrapper));
