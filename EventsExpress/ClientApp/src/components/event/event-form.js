import React, { Component } from 'react';
import { reduxForm, Field } from 'redux-form';
import { connect } from 'react-redux';
import moment from 'moment';
import Button from "@material-ui/core/Button";
import 'react-widgets/dist/css/react-widgets.css'
import momentLocaliser from 'react-widgets-moment';
import DropZoneField from '../helpers/DropZoneField';
import Module from '../helpers';
import periodicity from '../../constants/PeriodicityConstants'
import {
    renderMultiselect,
    renderTextArea,
    renderSelectPeriodicityField,
    renderCheckbox,
    renderTextField,
    renderDatePicker,
    radioLocationType
} from '../helpers/helpers';
import Inventory from '../inventory/inventory';
import LocationMap from './map/location-map';
import { enumLocationType } from '../../constants/EventLocationType';

momentLocaliser(moment);
const { validate } = Module;

const required = value => value ? undefined : "Required";

class EventForm extends Component {

    state = { checked: false };


    handleChange = () => {
        this.setState(state => ({
            checked: !state.checked,
        }));
    }


    render() {
        const { form_values, all_categories, isCreated, pristine,
            submitting, disabledDate, onCancel } = this.props;
        const { checked } = this.state;
        const { handleChange } = this;

        let values = form_values || this.props.initialValues;
        const photoUrl = this.props.initialValues ?
            this.props.initialValues.photoUrl : null;

        return (
            <form onSubmit={this.props.handleSubmit}
                encType="multipart/form-data" autoComplete="off" >
                <div className="text text-2 pl-md-4">

                    <Field
                        id="image-field"
                        name="image"
                        component={DropZoneField}
                        type="file"
                        crop={true}
                        cropShape='rect'
                        photoUrl={photoUrl}
                        validate={[required]}
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
                    <div className="mt-2">
                        <Field
                            name="categories"
                            component={renderMultiselect}
                            data={all_categories.data}
                            valueField={"id"}
                            textField={"name"}
                            className="form-control mt-2"
                            placeholder='#hashtags' />
                    </div>
                    <div>
                    </div>


                    <Field name="location.type" component={radioLocationType} />
                    {(this.props.form_values == undefined
                        || (this.props.form_values.location
                            && this.props.form_values.location.type === enumLocationType.map))
                        &&

                        <div className="mt-2">
                            <Field
                                name='location.selectedPos'
                                initialData={
                                    this.props.initialValues &&
                                    this.props.initialValues.location.selectedPos
                                }

                                component={LocationMap}
                            />
                        </div>
                    }
                    {this.props.form_values
                        && this.props.form_values.location
                        && this.props.form_values.location.type === enumLocationType.online &&

                        <div className="mt-2">
                            <label for="url">Enter an https:// URL:</label>
                            <Field
                                name='location.onlineMeeting'
                                component={renderTextField}
                                type="url"
                                label="Url"

                            />
                        </div>
                    }
                    {isCreated ? null : <Inventory />}
                </div>
                <div className="row pl-md-4">
                    <div className="col">
                        <Button
                            className="border"
                            fullWidth={true}
                            type="submit"
                            color="primary"
                            disabled={pristine || submitting}>
                            Save
                        </Button>
                    </div>
                    <div className="col">
                        <Button
                            className="border"
                            fullWidth={true}
                            color="primary"
                            onClick={onCancel}>
                            Cancel
                        </Button>
                    </div>
                </div>
            </form>
        );
    }
}

const mapStateToProps = (state) => ({
    initialData: state.event.data
});

EventForm = connect(
    mapStateToProps
)(EventForm);

export default reduxForm({
    form: 'event-form',
    validate: validate,
    enableReinitialize: true
})(EventForm);