import React, { Component } from 'react';
import { connect } from 'react-redux';
import { parse as queryStringParse } from 'query-string';
import EventList from '../components/Draft/Draft-list';
import Spinner from '../components/spinner';
import { get_drafts } from '../actions/event-list-action';
import InternalServerError from '../components/Route guard/500';
import BadRequest from '../components/Route guard/400';
import Unauthorized from '../components/Route guard/401';
import Forbidden from '../components/Route guard/403';
import history from '../history';
import eventHelper from '../components/helpers/eventHelper';


class EventDraftListWrapper extends Component {
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
        const { data, isPending, isError } = this.props.events;
        const { items } = this.props.events.data;
        const errorMessage = isError.ErrorCode == '403'
            ? <Forbidden />
            : isError.ErrorCode == '500'
                ? <InternalServerError />
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
        get_events: (filter) => dispatch(get_drafts(filter)),
    }
};

export default connect(
    mapStateToProps,
    mapDispatchToProps
)(EventDraftListWrapper);
