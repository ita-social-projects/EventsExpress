import React, {Component} from 'react';
import { EventForm } from '../components/event/event-form';
import add_event from '../actions/add-event';
import { connect } from 'react-redux';

class AddEventWrapper extends Component{
    
    onSubmit = (values) => {
        console.log(values);
        this.props.add_event({ ...values, user_id: this.props.user_id });
    }

    render(){   
    
        return <>
                <EventForm onSubmit={this.onSubmit} />
               </>
    }
}

const mapStateToProps = (state) => ({user_id: state.user.id, add_event: state.add_event});

const mapDispatchToProps = (dispatch) => { 
    return {
        add_event: (data) => dispatch(add_event(data))
    } 
};

export default connect(mapStateToProps, mapDispatchToProps)(AddEventWrapper);