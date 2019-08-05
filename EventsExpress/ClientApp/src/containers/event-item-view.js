import React, {Component} from 'react';
import { connect } from 'react-redux';
import EventItemView from '../components/event/event-item-view';
import Spinner from '../components/spinner';
import get_event from '../actions/event-item-view';
import { join, leave, resetEvent } from '../actions/event-item-view';



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

    render(){   
    
        const {data, isPending, isError} = this.props.event;
        // const hasData = !(isPending || isError);

        // const errorMessage = isError ? <ErrorIndicator/> : null;
        const spinner = isPending ? <Spinner /> : null;
        const content = !isPending ? <EventItemView onLeave={this.onLeave} onJoin={this.onJoin} data={data} current_user={this.props.current_user} /> : null;
    
        return <>
                {spinner}
                {content}
               </>
    }
}

const mapStateToProps = (state) => ({event: state.event, current_user: state.user});

const mapDispatchToProps = (dispatch) => { 
    return {
        get_event: (id) => dispatch(get_event(id)),
        join: (userId, eventId) => dispatch(join(userId, eventId)),
        leave: (userId, eventId) => dispatch(leave(userId, eventId)),
        reset: () => dispatch(resetEvent())
    } 
};

export default connect(mapStateToProps, mapDispatchToProps)(EventItemViewWrapper);