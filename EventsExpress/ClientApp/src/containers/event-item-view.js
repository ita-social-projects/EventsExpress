import React, { Component } from 'react';
import { connect } from 'react-redux';
import EventItemView from '../components/event/event-item-view';
import eventStatusEnum from '../constants/eventStatusEnum';
import Spinner from '../components/spinner';
import get_event, {
    join,
    leave,
    resetEvent,
    change_event_status
}
    from '../actions/event/event-item-view-action';
import get_unitsOfMeasuring from '../actions/unitOfMeasuring/unitsOfMeasuring-list-action';
import { get_inventories_by_event_id } from '../actions/inventory/inventory-list-action';
import { get_users_inventories_by_event_id, } from '../actions/users/users-inventories-action';

class EventItemViewWrapper extends Component {
    componentWillMount() {
        const { id } = this.props.match.params;
        this.props.get_event(id);
        this.props.get_unitsOfMeasuring();
        this.props.get_inventories_by_event_id(id);
        this.props.get_users_inventories_by_event_id(id);
    }

    componentWillUnmount() {
        this.props.reset();
    }

    onJoin = () => {
        this.props.join(this.props.current_user.id, this.props.event.data.id);
    }

    onLeave = () => {
        this.props.leave(this.props.current_user.id, this.props.event.data.id);
    }

    onCancel = (reason, eventStatus) => {
        this.props.cancel(this.props.event.data.id, reason, eventStatus);
    }

    onUnCancel = (reason, eventStatus) => {
        this.props.unCancel(this.props.event.data.id, reason, eventStatus);
    }
    onDelete = (reason, eventStatus) => {
        this.props.delete(this.props.event.data.id, reason, eventStatus);
    }

    render() {
        const { data } = this.props.event;

        return <Spinner showContent={data!=undefined}>
            <EventItemView
                event={this.props.event}
                match={this.props.match}
                onLeave={this.onLeave}
                onJoin={this.onJoin}
                onCancel={this.onCancel}
                onUnCancel={this.onUnCancel}
                onDelete={this.onDelete}
                current_user={this.props.current_user}
            />
        </Spinner>
    }
}

const mapStateToProps = (state) => ({
    event: state.event,
    current_user: state.user,
});

const mapDispatchToProps = (dispatch) => ({
    get_event: (id) => dispatch(get_event(id)),
    join: (userId, eventId) => dispatch(join(userId, eventId)),
    leave: (userId, eventId) => dispatch(leave(userId, eventId)),
    cancel: (eventId, reason) => dispatch(change_event_status(eventId, reason, eventStatusEnum.Canceled)),
    unCancel: (eventId, reason) => dispatch(change_event_status(eventId, reason, eventStatusEnum.Active)),
    delete: (eventId, reason) => dispatch(change_event_status(eventId, reason, eventStatusEnum.Deleted)),
    get_users_inventories_by_event_id: (eventId) => dispatch(get_users_inventories_by_event_id(eventId)),
    get_inventories_by_event_id: (eventId) => dispatch(get_inventories_by_event_id(eventId)),
    get_unitsOfMeasuring: () => dispatch(get_unitsOfMeasuring()),
    reset: () => dispatch(resetEvent())
});


export default connect(mapStateToProps, mapDispatchToProps)(EventItemViewWrapper);
