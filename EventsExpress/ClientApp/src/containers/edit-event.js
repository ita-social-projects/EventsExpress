import React, { Component } from 'react';
import EventForm from '../components/event/event-form';
import { withRouter } from "react-router";
import { connect } from 'react-redux';
import { getFormValues, reset } from 'redux-form';
import { setEventPending, setEventSuccess, edit_event } from '../actions/event/event-add-action';
import { setSuccessAllert } from '../actions/alert-action';
import { validate, validateEventForm  } from '../components/helpers/helpers'
import get_categories from '../actions/category/category-list-action';
import Button from "@material-ui/core/Button";

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
        await this.props.edit_event({ ...validateEventForm(values), user_id: this.props.user_id, id: this.props.event.id });
        this.props.alert('Your event has been successfully saved!');    
        this.props.history.goBack();
    }

    render() {
        return <>
            <div className="pl-md-4">
                <EventForm
                    validate={validate}
                    all_categories={this.props.all_categories}
                    onSubmit={this.onSubmit}
                    initialValues={this.props.event}
                    form_values={this.props.form_values}
                    haveReccurentCheckBox={false}
                    eventId={this.props.event.id}>
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
                            onClick={this.props.history.goBack}>
                            Cancel
                        </Button>
                    </div>
                </EventForm>
            </div>
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
        edit_event: (data) => dispatch(edit_event(data)),
        get_categories: () => dispatch(get_categories()),
        alert: (msg) => dispatch(setSuccessAllert(msg)),
        reset: () => {
            dispatch(reset('event-form'));
            dispatch(setEventPending(true));    
            dispatch(setEventSuccess(false));
        }
    }
};

export default withRouter(connect(
    mapStateToProps, 
    mapDispatchToProps
)(EditEventWrapper));

