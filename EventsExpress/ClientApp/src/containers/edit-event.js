import React, { Component } from 'react';
import EventForm from '../components/event/event-form';
import { connect } from 'react-redux';
import { getFormValues, reset } from 'redux-form';
import { setEventPending, setEventSuccess, edit_event } from '../actions/event/event-add-action';
import { validateEventForm } from '../components/helpers/helpers'
import { resetEvent } from '../actions/event/event-item-view';
import get_categories from '../actions/category/category-list-action';
import L from 'leaflet';

class EditEventWrapper extends Component {

    componentWillMount = () => {
        this.props.get_categories();
    }

    componentDidUpdate = () => {
        if (!this.props.add_event_status.errorEvent && this.props.add_event_status.isEventSuccess) {
            this.props.reset();
        }
    }

    componentWillUnmount() {
        this.props.reset();
    }

    onSubmit = (values) => {
        return this.props.add_event({ ...validateEventForm(values), user_id: this.props.user_id, id: this.props.event.id });
    }

    render() {
        let initialValues = {
            ...this.props.event,
            location: {
                selectedPos: L.latLng(
                    this.props.event.location.latitude,
                    this.props.event.location.longitude),
                onlineMeeting: this.props.event.location.onlineMeeting,
                type: String(this.props.event.location.type)
            },
        }

        return <>
            <EventForm
                all_categories={this.props.all_categories}
                onCancel={this.props.onCancelEditing}
                onSubmit={this.onSubmit}
                initialValues={initialValues}
                form_values={this.props.form_values}
                checked={this.props.event.isReccurent}
                haveReccurentCheckBox={false}
                haveMapCheckBox={true}
                haveOnlineLocationCheckBox={true}
                disabledDate={false}
                isCreated={true} />
        </>
    }
}

const mapStateToProps = (state) => ({
    user_id: state.user.id,
    add_event_status: state.add_event,
    all_categories: state.categories,
    form_values: getFormValues('event-form')(state),
    event: state.event.data,
});

const mapDispatchToProps = (dispatch) => {
    return {
        add_event: (data) => dispatch(edit_event(data)),
        get_categories: () => dispatch(get_categories()),
        resetEvent: () => dispatch(resetEvent()),
        reset: () => {
            dispatch(reset('event-form'));
            dispatch(setEventPending(true));
            dispatch(setEventSuccess(false));
        }
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(EditEventWrapper);