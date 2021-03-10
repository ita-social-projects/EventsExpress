import React, { Component } from 'react';
import { connect } from 'react-redux';
import { reset_events, updateEventsFilters } from '../../actions/event-list-action';
import RenderList from './RenderList'
import EventCard from './event-item';

class EventList extends Component {
    handlePageChange = (page) => {
        this.props.updateEventsFilters({
            ...this.props.filter,
            page: page,
        });
    };

    renderSingleItem = (item) => (
        <EventCard
            key={item.id + item.isBlocked}
            item={item}
            current_user={this.props.current_user}
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
    }
};

export default connect(
    null,
    mapDispatchToProps
)(EventList);
