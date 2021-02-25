import React, { Component } from 'react';
import EventForm from './event-form';
import { draft_event } from '../../actions/add-event';
import { connect } from 'react-redux';
import { getFormValues, reset } from 'redux-form';
import { setEventError, setEventPending, setEventSuccess } from '../../actions/add-event';
import { validateEventForm } from '../helpers/helpers'
import { resetEvent } from '../../actions/event-item-view';
import get_categories from '../../actions/category/category-list';
import L from 'leaflet';

class EditEventNew extends Component {

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
        return this.props.add_event({ ...validateEventForm(values), user_id: this.props.user_id, id: this.props.id });
    }

    render() {
        let initialValues = {
            ...this.props.event,
            selectedPos: L.latLng(
                this.props.event.latitude,
                this.props.event.longitude)
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
                disabledDate={false}
                isCreated={true} />
        </>
    }
}

const mapStateToProps = (state) => {
    return {
        user_id: state.user.id,
        add_event_status: state.add_event,
        all_categories: state.categories,
        form_values: getFormValues('event-form')(state),
        id: state.lastEventId,
        event: state.event.data,
    }
};
    

const mapDispatchToProps = (dispatch) => {
    return {
        add_event: (data) => dispatch(draft_event(data)),
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

export default connect(mapStateToProps, mapDispatchToProps)(EditEventNew);