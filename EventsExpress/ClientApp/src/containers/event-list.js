import React, { Component } from 'react';
import { getFormValues } from 'redux-form';
import { connect } from 'react-redux';
import EventList from '../components/event/event-list';
import SpinnerWrapper from './spinner';
import { get_events } from '../actions/event/event-list-action';
import { withRouter } from "react-router";

class EventListWrapper extends Component {
    constructor(props) {
        super(props);
        this.prevQueryStringSearch = '';
    }

    componentDidMount() {
        const query = this.props.history.location.search;
        this.props.get_events(query);
    }

    componentDidUpdate() {
        const query = this.props.history.location.search;
        if (query !== this.prevQueryStringSearch) {
            this.prevQueryStringSearch = query;
            this.props.get_events(query);
        }
    }

    render() {
        let current_user = this.props.current_user.id !== null
            ? this.props.current_user
            : {};
        const { data } = this.props.events;
        const { items } = this.props.events.data;

        return (
            <SpinnerWrapper showContent={data != undefined}>
                <EventList
                    current_user={current_user}
                    data_list={items}
                    filter={this.props.events.filter}
                    page={data.pageViewModel.pageNumber}
                    totalPages={data.pageViewModel.totalPages}
                    customNoResultsMessage="No events meet the specified criteria. Please make another choice."
                />
            </SpinnerWrapper>
        );
    }
}

const mapStateToProps = (state) => {
    return {
        events: state.events,
        current_user: state.user,
        // TODO: delete, possible dead code
        form_values: getFormValues('event-filter-form')(state)
        //
    };
};

const mapDispatchToProps = (dispatch) => {
    return {
        get_events: (filter) => dispatch(get_events(filter))
    };
};

export default withRouter(connect(
    mapStateToProps,
    mapDispatchToProps
)(EventListWrapper));
