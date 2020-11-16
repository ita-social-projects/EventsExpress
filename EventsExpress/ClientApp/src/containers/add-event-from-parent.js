import React, { Component } from 'react';
import EventForm from '../components/event/event-form';
import { edit_event } from '../actions/add-event';
import get_countries from '../actions/countries';
import { connect } from 'react-redux';
import { getFormValues, reset } from 'redux-form';
import get_cities from '../actions/cities';
import { setEventError, setEventPending, setEventSuccess } from '../actions/add-event';
import { resetEvent } from '../actions/event-item-view';
import get_categories from '../actions/category-list';

class AddFromParentEventWrapper extends Component {

    componentDidUpdate = () => {
        if (!this.props.add_event_status.errorEvent && this.props.add_event_status.isEventSuccess) {
            this.props.reset();
        }
    }

    componentWillUnmount() {
        this.props.reset();
    }

    onSubmit = () => {

        if (!this.props.initialValues.maxParticipants) {
            this.props.initialValues.maxParticipants = 2147483647;
        }

        if (!this.props.initialValues.dateFrom) {
            this.props.initialValues.dateFrom = new Date(Date.now());
        }

        if (!this.props.initialValues.dateTo) {
            this.props.initialValues.dateTo = new Date(this.props.initialValues.dateFrom);
        }

        this.props.add_event({ ...this.props.initialValues, user_id: this.props.user_id });

    }

    render() {
        return <>

        </>
    }
}

const mapStateToProps = (state) => ({
    user_id: state.user.id,
    add_event_status: state.add_event,
    countries: state.countries,
    selectedCountries: state.occurenceEvent.data.event.countryName,
    cities: state.cities,
    all_categories: state.categories.data,
    initialValues: state.occurenceEvent.data.event,
});

const mapDispatchToProps = (dispatch) => {
    return {
        add_event: (data) => dispatch(edit_event(data)),
        get_countries: () => dispatch(get_countries()),
        get_cities: (country) => dispatch(get_cities(country)),
        get_categories: () => dispatch(get_categories()),
        resetEvent: () => dispatch(resetEvent()),
        reset: () => {
            dispatch(reset('event-form'));
            dispatch(setEventPending(true));
            dispatch(setEventSuccess(false));
            dispatch(setEventError(null));
        }
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(AddFromParentEventWrapper);