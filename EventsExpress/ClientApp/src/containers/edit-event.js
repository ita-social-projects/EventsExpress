import React, { Component } from 'react';
import EventForm from '../components/event/event-form';
import { withRouter } from 'react-router';
import { connect } from 'react-redux';
import { getFormValues, SubmissionError } from 'redux-form';
import { edit_event } from '../actions/event/event-add-action';
import { setSuccessAllert } from '../actions/alert-action';
import { validate } from './event-edit-validate-form ';
import { validateEventForm } from './event-validate-form';
import Button from '@material-ui/core/Button';
import { buildValidationState } from '../components/helpers/action-helpers';

class EditEventWrapper extends Component {

    onSubmit = async (values) => {
        await this.props.edit_event({
            ...validateEventForm(values),
            user_id: this.props.user_id,
            id: this.props.event.id
        });

        this.props.history.goBack();
    };

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
        </>;
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
        edit_event: (data) => dispatch(edit_event(data, async response => {
            throw new SubmissionError(await buildValidationState(response));
        }, dispatch(setSuccessAllert('Your event has been successfully saved!')))),
        alert: (msg) => dispatch(setSuccessAllert(msg))
    };
};

export default withRouter(connect(
    mapStateToProps,
    mapDispatchToProps
)(EditEventWrapper));

