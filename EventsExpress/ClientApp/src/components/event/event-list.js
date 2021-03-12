import React, { Component } from 'react';
import { connect } from 'react-redux';
import { reset_events, updateEventsFilters } from '../../actions/event-list-action';
import RenderList from './RenderList'
import EventCard from './event-item';
import { change_event_status } from '../../actions/event-item-view';
import eventStatusEnum from '../helpers/eventStatusEnum';

class EventList extends Component {
    handlePageChange = (page) => {
        this.props.updateEventsFilters({
            ...this.props.filter,
            page: page,
        });
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

    render(){
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

export default connect(
    null,
    mapDispatchToProps
)(EventList);
