import React, {Component} from 'react';
import { connect } from 'react-redux';
import EventItemView from '../components/event/event-item-view';
import Spinner from '../components/spinner';
import get_event from '../actions/event-item-view';
import { join, leave, resetEvent, cancel_event } from '../actions/event-item-view';



class EventItemViewWrapper extends Component{
    
    componentWillMount(){    
        const { id } = this.props.match.params;
        this.props.get_event(id);
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

    onCancel = () => {
        this.props.cancel(this.props.event.data.id, "By some reason");
    }

    render(){   
        const {data, isPending } = this.props.event;
  
        return isPending
            ? <Spinner />
            : <EventItemView 
                data={data}
                match={this.props.match} 
                onLeave={this.onLeave} 
                onJoin={this.onJoin}
                onCancel={this.onCancel}
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
    join: (userId, eventId) => dispatch(join(userId, eventId)),
    leave: (userId, eventId) => dispatch(leave(userId, eventId)),
    cancel: (eventId, reason) => dispatch(cancel_event(eventId, reason)),
    reset: () => dispatch(resetEvent())
})


export default connect(mapStateToProps, mapDispatchToProps)(EventItemViewWrapper);