import React, { Component } from 'react';
import EventForm from '../components/event/event-form';
import { edit_event, publish_event } from '../actions/add-event';
import { connect } from 'react-redux';
import { getFormValues, reset } from 'redux-form';
import { setEventError, setEventPending, setEventSuccess } from '../actions/add-event';
import { validate, validateEventForm } from '../components/helpers/helpers'
import { resetEvent } from '../actions/event-item-view';
import get_categories from '../actions/category/category-list';
import L from 'leaflet';
import Button from "@material-ui/core/Button";
// link for me
class EventDraftWrapper extends Component {

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

    // check how to work with async validation
    // setAlert check how its work  
    render() {
        let initialValues = {
            ...this.props.event,
            selectedPos: L.latLng(
                this.props.event.latitude,
                this.props.event.longitude)
        }

        return <>
            <EventForm
                validate={validate}
                all_categories={this.props.all_categories}
                onCancel={this.props.onCancelEditing}
                onPublish={this.onPublish}
                onSubmit={this.onSubmit}
                initialValues={initialValues}
                form_values={this.props.form_values}
                checked={this.props.event.isReccurent}
                haveReccurentCheckBox={false}
                disabledDate={false}
                isCreated={true}>
                <div className="col">
                    <Button
                        className="border"
                        fullWidth={true}
                        color="primary"
                        type="submit"
                    >
                        Save
                        </Button>
                </div>
                <div className="col">
                    <Button
                        className="border"
                        fullWidth={true}
                        color="primary"
                        onClick={this.handleClick}>
                        Cancel
                        </Button>
                </div>
            </EventForm>
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
            dispatch(setEventError(null));
        }
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(EventDraftWrapper);

