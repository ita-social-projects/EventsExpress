import React, { Component } from 'react';
import EventForm from '../components/event/event-form';
import { connect } from 'react-redux';
import { getFormValues, reset } from 'redux-form';
import edit_event_from_parent, {
    setEventFromParentPending,
    setEventFromParentSuccess
}
    from '../actions/event/event-copy-with-edit-action';
import * as moment from 'moment';
import { validate } from '../components/helpers/helpers'
import { validateEventForm } from '../components/helpers/validators/event-validate-form'
import get_categories from '../actions/category/category-list-action';
import Button from "@material-ui/core/Button";

class EditFromParentEventWraper extends Component {

    componentWillMount = () => {
        this.props.get_categories();
    }

    componentDidUpdate = () => {
        if (!this.props.edit_event_from_parent_status.eventFromParentError &&
            this.props.edit_event_from_parent_status.isEventFromParentSuccess) {
            this.props.reset();
        }
    }

    componentWillUnmount() {
        this.props.reset();
    }

    onSubmit = (values) => {
        if (values.isReccurent) {
            values.isReccurent = false;
        }
        this.props.edit_event_from_parent({ ...validateEventForm(values), user_id: this.props.user_id });
    }

    render() {
        let initialValues = {
            ...this.props.event,
            dateFrom: this.props.eventSchedule.nextRun,
            dateTo: new moment(this.props.event.dateTo)
                .add(new moment(this.props.eventSchedule.nextRun)
                    .diff(new moment(this.props.event.dateFrom), 'days'), 'days')
        }
        return <>
            <EventForm
                validate={validate}
                all_categories={this.props.all_categories}
                onSubmit={this.onSubmit}
                initialValues={initialValues}
                form_values={this.props.form_values}
                haveReccurentCheckBox={false}
                disabledDate={true}
                eventId={this.props.event.id}>
                <div className="col">
                    <Button
                        className="border"
                        fullWidth={true}
                        color="primary"
                        type="submit">
                        Publish
                    </Button>
                </div>
                <div className="col">
                    <Button
                        className="border"
                        fullWidth={true}
                        color="primary"
                        onClick={this.props.onCancelEditing}>
                        Cancel
                    </Button>
                </div>
            </EventForm>
        </>
    }
}

const mapStateToProps = (state) => ({
    user_id: state.user.id,
    edit_event_from_parent_status: state.edit_event_from_parent,
    all_categories: state.categories,
    form_values: getFormValues('event-form')(state),
    eventSchedule: state.eventSchedule.data,
    event: state.event.data,
});

const mapDispatchToProps = (dispatch) => {
    return {
        edit_event_from_parent: (data) => dispatch(edit_event_from_parent(data)),
        get_categories: () => dispatch(get_categories()),
        reset: () => {
            dispatch(reset('event-form'));
            dispatch(setEventFromParentPending(true));
            dispatch(setEventFromParentSuccess(false));
        }
    }
};

export default connect(mapStateToProps, mapDispatchToProps)(EditFromParentEventWraper);
