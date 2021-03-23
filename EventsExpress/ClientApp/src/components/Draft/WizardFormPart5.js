import React, { Component } from 'react';
import { reduxForm, Field, getFormValues, } from 'redux-form';
import { connect } from 'react-redux';
import { setEventPending, setEventSuccess, publish_event, edit_event_part5 } from '../../actions/event-add-action';
import { validateEventFormPart5 } from '../helpers/helpers'
import Button from "@material-ui/core/Button";
import {
    renderCheckbox,
    renderTextField,
} from '../helpers/helpers'

class Part5 extends Component {

    onSubmit = () => {
        return this.props.add_event({ ...validateEventFormPart5(this.props.form_values), user_id: this.props.user_id, id: this.props.event.id });
    }
    initializeIfNeed() {
        if (this.props.event) {
            let initialValues = {
                isPublic: this.props.event.isPublic,
                maxParticipants: this.props.event.maxParticipants,
            }
            this.props.initialize(initialValues);
            this.setState({ initialized: true });
        }
    }

    componentDidMount() {
        this.initializeIfNeed();
    }

    render() {
        return (
            <form onSubmit={this.props.handleSubmit(this.onSubmit)}
                encType="multipart/form-data" autoComplete="off"  >
                <div className="mt-2">
                    <Field
                        name='isPublic'
                        component={renderCheckbox}
                        type="checkbox"
                        label="Public"
                    />
                </div>
                <div className="mt-2">
                    <Field
                        name='maxParticipants'
                        component={renderTextField}
                        type="number"
                        label="Max Count Of Participants"
                    />
                </div>
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
            </form >
        );

    }
}

const mapStateToProps = (state) => ({
    initialData: state.event.data,
    user_id: state.user.id,
    all_categories: state.categories,
    form_values: getFormValues('Part5')(state),
    event: state.event.data,
});

const mapDispatchToProps = (dispatch) => {
    return {
        add_event: (data) => dispatch(edit_event_part5(data)),
        publish: (data) => dispatch(publish_event(data)),
        get_categories: () => dispatch(get_categories()),
        resetEvent: () => dispatch(resetEvent()),
        reset: () => {
            dispatch(reset('Part5'));
            dispatch(setEventPending(true));
            dispatch(setEventSuccess(false));
        }
    }
};

Part5 = connect(
    mapStateToProps,
    mapDispatchToProps
)(Part5);

export default reduxForm({
    form: 'Part5',
    enableReinitialize: true
})(Part5);

