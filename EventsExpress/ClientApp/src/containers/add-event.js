import React, { Component } from 'react';
import EventForm from '../components/event/event-form';
import { connect } from 'react-redux';
import { getFormValues, reset } from 'redux-form';
import add_event from '../actions/event/event-add-action';
import { getRequestInc, getRequestDec } from "../actions/request-count-action";
import { setAlert } from '../actions/alert-action';
import get_categories from '../actions/category/category-list-action';
import { validateEventForm } from '../components/helpers/helpers';
import { enumLocationType } from '../constants/EventLocationType';

class AddEventWrapper extends Component {

    state = {
        open: false
    }

    componentDidMount = () => {

        this.props.get_categories();
    }

    componentDidUpdate = () => {
        if (!this.props.add_event_status.errorEvent && !this.props.add_event_status.counter)  {
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
        return this.props.add_event({ ...validateEventForm(values), user_id: this.props.user_id });
    }

    handleClose = () => {
        this.setState({ open: false });
    }


    render() {
        if (!this.props.add_event_status.counter) {
            this.setState({ open: true });
        }
        let initialValues = {
            location: {
                type: enumLocationType.map
            },
            selectedPos: {
                lat: 50.4547,
                lng: 30.5238
            }
        };

        return <div className="w-50 m-auto pb-4 pt-4">
            <EventForm data={{}}
                all_categories={this.props.all_categories}
                onCancel={this.props.onCreateCanceling}
                onSubmit={this.onSubmit}
                form_values={this.props.form_values}
                disabledDate={false}
                haveReccurentCheckBox={true}
                haveMapCheckBox={true}
                haveOnlineLocationCheckBox={true}
                addEventStatus={this.props.add_event_status}
                initialValues={initialValues}
                isCreated={false} />
        </div>
    }
}

const mapStateToProps = (state) => ({
    user_id: state.user.id,
    add_event_status: state.add_event,
    all_categories: state.categories,
    form_values: getFormValues('event-form')(state),
    counter: state.requestCount.counter
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
            dispatch(getRequestInc());
            dispatch(getRequestDec());
        }
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(AddEventWrapper);