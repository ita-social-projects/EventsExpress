import React, { Component } from 'react';
import { reduxForm, Field, getFormValues, reset, isPristine } from 'redux-form';
import { connect } from 'react-redux';
import { setEventPending, setEventSuccess, edit_event, publish_event } from '../../actions/event-add-action';
import moment from 'moment';
import { validateEventForm } from '../helpers/helpers'
import Button from "@material-ui/core/Button";
import 'react-widgets/dist/css/react-widgets.css'
import momentLocaliser from 'react-widgets-moment';
import DropZoneField from '../helpers/DropZoneField';
import periodicity from '../../constants/PeriodicityConstants'
import {
    renderTextArea,
    renderSelectPeriodicityField,
    renderCheckbox,
    renderTextField,
    renderDatePicker,
} from '../helpers/helpers';
import { createBrowserHistory } from 'history';

momentLocaliser(moment);
const history = createBrowserHistory({ forceRefresh: true });

class TestForm extends Component {

    state = { checked: false };


    handleChange = () => {
        this.setState(state => ({
            checked: !state.checked,
        }));

    }
    handleClick = () => {
        history.push(`/`);
    }

    onSubmit = () => {
        return this.props.add_event({ ...validateEventForm(this.props.form_values), user_id: this.props.user_id, id: this.props.event.id });
    }
   


    render() {
        const { form_values, disabledDate, } = this.props;
        const { checked } = this.state;
        const { handleChange } = this;

        let values = form_values || this.props.initialValues;
        const photoUrl = this.props.initialValues ?
            this.props.initialValues.photoUrl : null;

        return (
            <form onSubmit={this.props.handleSubmit(this.onSubmit)}
                encType="multipart/form-data" autoComplete="off" >
                <div className="text text-2 pl-md-4">

                    <Field
                        id="image-field"
                        name="photo"
                        component={DropZoneField}
                        type="file"
                        crop={true}
                        cropShape='rect'
                        photoUrl={photoUrl}
                    />
                    <div className="mt-2">
                        <Field name='title'
                            component={renderTextField}
                            type="input"
                            label="Title"
                            inputProps={{ maxLength: 60 }}
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
                    {this.props.haveReccurentCheckBox &&
                        <div className="mt-2">
                            <br />
                            <Field
                                type="checkbox"
                                label="Recurrent Event"
                                name='isReccurent'
                                component={renderCheckbox}
                                checked={checked}
                                onChange={handleChange} />
                        </div>
                    }
                    {checked &&
                        <div>
                            <div className="mt-2">
                                <Field
                                    name="periodicity"
                                    text="Periodicity"
                                    data={periodicity}
                                    component={renderSelectPeriodicityField} />
                            </div>
                            <div className="mt-2">
                                <Field
                                    name='frequency'
                                    type="number"
                                    component={renderTextField} />
                            </div>
                        </div>
                    }
                    <div className="mt-2">
                        <Field
                            name='isPublic'
                            component={renderCheckbox}
                            type="checkbox"
                            label="Public"
                        />
                    </div>
                    <div className="meta-wrap m-2">
                        <span>From
                            <Field
                                name='dateFrom'
                                component={renderDatePicker}
                                disabled={disabledDate ? true : false}
                            />
                        </span>
                        {values && values.dateFrom &&
                            <span>To
                                <Field
                                    name='dateTo'
                                    minValue={values.dateFrom}
                                    component={renderDatePicker}
                                    disabled={disabledDate ? true : false}
                                />
                            </span>
                        }
                    </div>
                    <div className="mt-2">
                        <Field
                            name='description'
                            component={renderTextArea}
                            type="input"
                            label="Description"
                        />
                    </div>
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
                
            </form>
        );
    }
}

const mapStateToProps = (state) => ({
    initialData: state.event.data,
    user_id: state.user.id,
    add_event_status: state.add_event,
    all_categories: state.categories,
    form_values: getFormValues('test-form')(state),
    pristine: isPristine('test-form')(state),
    event: state.event.data,
});

const mapDispatchToProps = (dispatch) => {
    return {
        add_event: (data) => dispatch(edit_event(data)),
        publish: (data) => dispatch(publish_event(data)),
        get_categories: () => dispatch(get_categories()),
        resetEvent: () => dispatch(resetEvent()),
        reset: () => {
            dispatch(reset('test-form'));
            dispatch(setEventPending(true));
            dispatch(setEventSuccess(false));
        }
    }
}

TestForm = connect(
    mapStateToProps,
    mapDispatchToProps
)(TestForm);

export default reduxForm({
    form: 'test-form',
    enableReinitialize: true
})(TestForm);