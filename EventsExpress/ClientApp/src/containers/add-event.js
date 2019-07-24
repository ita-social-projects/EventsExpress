import React, {Component} from 'react';
import { EventForm } from '../components/event/event-form';
import add_event from '../actions/add-event';
import get_countries from '../actions/countries';
import { connect } from 'react-redux';
import {getFormValues, reset} from 'redux-form';
import get_cities from '../actions/cities';
import { setEventError, setEventPending, setEventSuccess } from '../actions/add-event';

class AddEventWrapper extends Component{
    
    componentDidMount = () =>{
        this.props.get_countries();
    }
    componentDidUpdate = () => {
        if(!this.props.add_event_status.errorEvent && this.props.add_event_status.isEventSuccess){
            this.props.reset();
        }
    }

    onSubmit = (values) => {
        this.props.add_event({ ...values, user_id: this.props.user_id });
        console.log(this.props.add_event_status);

    }

    onChangeCountry = (e) => {
        this.props.get_cities(e.target.value);
    }

    render(){   
        return <>
                <EventForm cities={this.props.cities.data} onChangeCountry={this.onChangeCountry} onSubmit={this.onSubmit} countries={this.props.countries.data} form_values={this.props.form_values} />
               </>
    }
}

const mapStateToProps = (state) => ({user_id: state.user.id,
     add_event_status: state.add_event, 
     countries: state.countries,
     cities: state.cities,
     form_values: getFormValues('event-form')(state)});

const mapDispatchToProps = (dispatch) => { 
    return {
        add_event: (data) => dispatch(add_event(data)),
        get_countries: () => dispatch(get_countries()),
        get_cities: (country) => dispatch(get_cities(country)),
        reset: () => {
            dispatch(reset('event-form'));
            dispatch(setEventPending(true));
            dispatch(setEventSuccess(false));
            dispatch(setEventError(null));
        }
    } 
};

export default connect(mapStateToProps, mapDispatchToProps)(AddEventWrapper);