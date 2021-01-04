import React, { Component } from 'react';
import EventForm from '../components/event/event-form';
import add_event from '../actions/add-event';
import { connect } from 'react-redux';
import { getFormValues, reset } from 'redux-form';
import { setEventError, setEventPending, setEventSuccess } from '../actions/add-event';
import { setAlert } from '../actions/alert';
import get_categories from '../actions/category/category-list';
import { validateEventForm } from '../components/helpers/helpers'

class AddEventWrapper extends Component {

    state = {
        open: false
    }

    componentDidMount = () => {

        this.props.get_categories();
    }

    componentDidUpdate = () => {
        if (!this.props.add_event_status.errorEvent && this.props.add_event_status.isEventSuccess) {
            this.props.reset();
            this.props.resetEventStatus();
            this.props.alert({ variant: 'success', message: 'Your event was created!', autoHideDuration: 5000 });
        }
    }

    componentWillUnmount = () => {
        this.props.reset();
        this.props.resetEventStatus();
    }

    onSubmit = (values) => {
        this.props.add_event({ ...validateEventForm(values), user_id: this.props.user_id });
    }

    handleClose = () => {
        this.setState({ open: false });
    }

    render() {
        console.log("state", this.props);
        if (this.props.add_event_status.isEventSuccess) {
            this.setState({ open: true });
        }

        return <div className="w-50 m-auto p-4">
            <EventForm data={{}}
                all_categories={this.props.all_categories}
                onCancel={this.props.onCreateCanceling}
                onSubmit={this.onSubmit}
                form_values={this.props.form_values}
                disabledDate={false}
                haveReccurentCheckBox={true}
                addEventStatus={this.props.add_event_status}
                isCreated={false} />
        </div>
    }
}

const mapStateToProps = (state) => ({
    user_id: state.user.id,
    add_event_status: state.add_event,
    all_categories: state.categories,
    form_values: getFormValues('event-form')(state)
});

const mapDispatchToProps = (dispatch) => {
    return {
        add_event: (data) => dispatch(add_event(data)),
        get_categories: () => dispatch(get_categories()),
        reset: () => {
            dispatch(reset('event-form'));
        },
        alert: (data) => dispatch(setAlert(data)),
        resetEventStatus: () => {
            dispatch(setEventPending(true));
            dispatch(setEventSuccess(false));
            dispatch(setEventError(null));
        }
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(AddEventWrapper);