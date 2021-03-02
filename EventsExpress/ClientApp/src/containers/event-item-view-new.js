import React, {Component} from 'react';
import { connect } from 'react-redux';
import EventItemViewNew from '../components/event/event-item-view-new';
import Spinner from '../components/spinner';
import get_event, { 
    join, 
    leave, 
    resetEvent, 
    cancel_event, 
    approveUser, 
    deleteFromOwners, 
    promoteToOwner
    }
from '../actions/event-item-view';

class EventItemViewWrapperNew extends Component{

    componentWillMount(){    
        const { id } = this.props.match.params;        
        this.props.get_event(id);
    }

    componentWillUnmount(){
        this.props.reset();
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
            : <EventItemViewNew
                event={this.props.event}
                match={this.props.match} 
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
    cancel: (eventId, reason) => dispatch(cancel_event(eventId, reason)),
    approveUser: (userId, eventId, buttonAction) => dispatch(approveUser(userId, eventId, buttonAction)),
    reset: () => dispatch(resetEvent())
});


export default connect(mapStateToProps, mapDispatchToProps)(EventItemViewWrapperNew);
