import React, { Component } from 'react';
import EventForm from '../components/event/event-form';
import { createBrowserHistory } from 'history';
import { connect } from 'react-redux';
import { getFormValues, reset } from 'redux-form';
import { setEventPending, setEventSuccess, edit_event} from '../actions/event/event-add-action';
import { validate, validateEventForm  } from '../components/helpers/helpers'
import { resetEvent } from '../actions/event/event-item-view-action';
import get_categories from '../actions/category/category-list-action';
import L from 'leaflet';
import Button from "@material-ui/core/Button";

const history = createBrowserHistory({ forceRefresh: true });

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

    onSubmit = async (values) => {
        await this.props.add_event({ ...validateEventForm(values), user_id: this.props.user_id, id: this.props.event.id });
        history.goBack();
    }

    onCancel = () => {
        history.goBack();
    }

    render() {
        let initialValues = {
            ...this.props.event,
            location: this.props.event.location !== null ? {
                selectedPos: L.latLng(

                    this.props.event.location.latitude,
                    this.props.event.location.longitude
                ),
                onlineMeeting: this.props.event.location.onlineMeeting,
                type: String(this.props.event.location.type)
            } : null,
        }

        return <>
            <EventForm
                validate={validate}
                all_categories={this.props.all_categories}
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
                        type="submit">
                        Save
                    </Button>
                </div>
                <div className="col">
                    <Button
                        className="border"
                        fullWidth={true}
                        color="primary"
                        onClick={this.onCancel}>
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
        }
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(EditEventWrapper);

