import React, { Component } from 'react';
import { connect } from 'react-redux';
import { reset_events, updateEventsFilters } from '../../actions/event/event-list-action';
import RenderList from './RenderList'
import EventCard from './event-item';
import { change_event_status } from '../../actions/event/event-item-view-action';
import eventStatusEnum from '../../constants/eventStatusEnum';
import { withRouter } from "react-router";
import { parse as queryStringParse } from 'query-string';
import filterHelper from '../helpers/filterHelper';

class EventList extends Component {
    handlePageChange = (page) => {
        if (this.props.history.location.search == "")
            this.props.history.push(this.props.history.location.pathname + `?page=${page}`);
        else {
            const queryStringToObject = queryStringParse(this.props.history.location.search);
            queryStringToObject.page = page;
            this.props.history.location.search = filterHelper.getQueryStringByFilter(queryStringToObject);
            this.props.history.push(this.props.history.location.pathname + this.props.history.location.search);
        }
    };


    renderSingleItem = (item) => (
        <EventCard
            key={item.id + item.Active}
            item={item}
            current_user={this.props.current_user}
            onBlock={this.props.onBlock}
            onUnBlock={this.props.onUnBlock}
        />
    )

    render() {
        return <>
            <RenderList {...this.props} renderSingleItem={this.renderSingleItem} handlePageChange={this.handlePageChange} />
        </>
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        reset_events: () => dispatch(reset_events()),
        updateEventsFilters: (filter) => dispatch(updateEventsFilters(filter)),
        onBlock: (eventId, reason) => dispatch(change_event_status(eventId, reason, eventStatusEnum.Blocked)),
        onUnBlock: (eventId, reason) => dispatch(change_event_status(eventId, reason, eventStatusEnum.Active))
    }
};

export default withRouter(connect(
    null,
    mapDispatchToProps
)(EventList));
