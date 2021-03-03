import React, { Component } from 'react';
import { connect } from 'react-redux';
import { reset_events, updateEventsFilters } from '../../actions/event-list-action';
import  { resetEvent } from '../../actions/event-item-view';
import RenderList from './ListRender'
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
    };
}

const mapDispatchToProps = (dispatch) => {
    return {
        reset_events: () => dispatch(reset_events()),
        updateEventsFilters: (filter) => dispatch(updateEventsFilters(filter)),
        reset: () => dispatch(resetEvent())
    }
};

export default connect(
    null,
    mapDispatchToProps
)(EventList);
