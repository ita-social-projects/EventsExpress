import React, {Component} from 'react';
import { connect } from 'react-redux';
import EventItemView from '../components/event/event-item-view';
import Spinner from '../components/spinner';
import get_event from '../actions/event-item-view';
import { get_inventories_by_event_id }  from '../actions/inventory-list';
import { join, leave, resetEvent, cancel_event, approveUser } from '../actions/event-item-view';

class EventItemViewWrapper extends Component{
    componentWillMount(){    
        const { id } = this.props.match.params;
        this.props.get_event(id);
        this.props.get_inventories_by_event_id(id);
    }

    componentWillUnmount(){
        this.props.reset();
    }

    onJoin = () => {
        this.props.join(this.props.current_user.id, this.props.event.data.id);
    }

    onLeave = () => {
        this.props.leave(this.props.current_user.id, this.props.event.data.id);
    }

    onCancel = (reason) => {
        this.props.cancel(this.props.event.data.id, reason);
    }

    onApprove = (userId, buttonAction) => {
        this.props.approveUser(userId, this.props.event.data.id, buttonAction);
    }

    render(){   
        const { isPending } = this.props.event;
        return isPending
            ? <Spinner />
            : <EventItemView
                event={this.props.event}
                match={this.props.match} 
                onLeave={this.onLeave} 
                onJoin={this.onJoin}
                onCancel={this.onCancel}
                onApprove={this.onApprove}
                current_user={this.props.current_user}
            />
    }
}

const mapStateToProps = (state) => ({
    event: state.event, 
    current_user: state.user
});

const mapDispatchToProps = (dispatch) => ({
    get_event: (id) => dispatch(get_event(id)),
    get_inventories_by_event_id: (eventId) => dispatch(get_inventories_by_event_id(eventId)),
    join: (userId, eventId) => dispatch(join(userId, eventId)),
    leave: (userId, eventId) => dispatch(leave(userId, eventId)),
    cancel: (eventId, reason) => dispatch(cancel_event(eventId, reason)),
    approveUser: (userId, eventId, buttonAction) => dispatch(approveUser(userId, eventId, buttonAction)),
    reset: () => dispatch(resetEvent())
})


export default connect(mapStateToProps, mapDispatchToProps)(EventItemViewWrapper);
