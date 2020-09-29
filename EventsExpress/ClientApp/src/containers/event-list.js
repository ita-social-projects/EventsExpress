import React, { Component } from 'react';
import { connect } from 'react-redux';
import EventList from '../components/event/event-list';
import Spinner from '../components/spinner';
import { get_events } from '../actions/event-list';
import BadRequest from '../components/Route guard/400';
import Unauthorized from '../components/Route guard/401';
import Forbidden from '../components/Route guard/403';
import { Redirect } from 'react-router';

class EventListWrapper extends Component {
    componentDidUpdate(prevProps, prevState) {
        if (this.props.events.isError.ErrorCode == '500') {
            this.getEvents(this.props.params);
        }
    }
    componentDidMount() {
        this.getEvents(this.props.params);
    }

    componentWillUpdate = (newProps) => {
        if (newProps.params !== this.props.params)
            this.getEvents(newProps.params);
    }

    getEvents = (filter) => this.props.get_events(filter);

    render() {
        let current_user = this.props.current_user.id != null ? this.props.current_user : {};
        const { data, isPending, isError } = this.props.events;
        const { items } = this.props.events.data;
        const errorMessage = isError.ErrorCode == '403'
            ? <Forbidden />
            : isError.ErrorCode == '500'
                ? <Redirect from="*" to="/home/events?page=1" />
                : isError.ErrorCode == '401'
                    ? <Unauthorized />
                    : isError.ErrorCode == '400'
                        ? <BadRequest />
                        : null;

        const spinner = isPending ? <Spinner /> : null;
        const content = !errorMessage ? <EventList current_user={current_user} data_list={items} page={data.pageViewModel.pageNumber} totalPages={data.pageViewModel.totalPages} callback={this.getEvents} /> : null;

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
        get_events: (filter) => dispatch(get_events(filter))
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(EventListWrapper);