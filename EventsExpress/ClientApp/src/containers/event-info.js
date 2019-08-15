import React, { Component } from 'react';
import { connect } from 'react-redux';
import {block_event,unblock_event} from '../actions/event-item-view';
import {EventBlock} from '../components/event/event-block';
import EventItemView from '../components/event/event-item-view';

class EventInfoWrapper extends Component{
    constructor(props){
        super(props);
        this.isCurrentUser=props.event.id===props.currentEvent
    }

    block = ()=>this.props.block(this.props.user.id)

    unblock=()=>this.props.unblock(this.props.event.id)

    render(){
        const{event, editedEvent}=this.props

        return(
            <tr className={(user.isBlocked==true)?"bg-warning":""}>
              <EventItemView key={event.id} event={event} />

              <EventBlock
                event={event}
                isCurrentUser={this.isCurrentUser}
                block={this.block}
                unblock={this.unblock}
              />  

            </tr>
        )
    }
}

const mapStateToProps=(state)=>({
    currentEvent:state.event.id,
    editedEvent:state.events.editedEvent,
    roles:state.roles.data
});

const mapDispatchToProps=(dispatch)=>{
    return{
        block:(id)=>dispatch(block_event(id)),
        unblock:(id)=>dispatch(unblock_event(id))
    };
}

export default connect(mapStateToProps,mapDispatchToProps)(EventInfoWrapper)