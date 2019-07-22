import React, {Component} from 'react';
import { EventForm } from '../components/event/event-form';
import add_event from '../actions/add-event';
import get_countries from '../actions/countries';
import { connect } from 'react-redux';

class AddEventWrapper extends Component{
    
    componentDidMount = () =>{
        this.props.get_countries();
    }

    onSubmit = (values) => {
        this.props.add_event({ ...values, user_id: this.props.user_id });
    }

    render(){   
    
        return <>
                <EventForm onSubmit={this.onSubmit} countries={this.props.countries.data} />
               </>
    }
}

const mapStateToProps = (state) => ({user_id: state.user.id, add_event: state.add_event, countries: state.countries});

const mapDispatchToProps = (dispatch) => { 
    return {
        add_event: (data) => dispatch(add_event(data)),
        get_countries: () => dispatch(get_countries())
    } 
};

export default connect(mapStateToProps, mapDispatchToProps)(AddEventWrapper);