import React, { Component } from 'react';
import { reduxForm, Field } from 'redux-form';
import moment from 'moment';
import 'react-widgets/dist/css/react-widgets.css'
import momentLocaliser from 'react-widgets-moment';
import DropZoneField from '../helpers/DropZoneField';
import periodicity from '../../constants/PeriodicityConstants'
import {
    renderMultiselect,
    renderTextArea,
    renderSelectPeriodicityField,
    renderCheckbox,
    renderTextField,
    radioLocationType
} from '../helpers/helpers';
import { renderDatePicker, renderLocationMapWithMarker } from '../helpers/form-helpers';
import { enumLocationType } from '../../constants/EventLocationType';
import "./event-form.css";

momentLocaliser(moment);

class EventForm extends Component {

    state = { checked: this.props.initialValues.isReccurent };

    handleChange = () => {
        this.setState(state => ({
            checked: !state.checked,
        }));
    }

    render() {
        const { form_values, all_categories } = this.props;
        const { checked } = this.state;
        
        if (this.props.initialValues.location != null) {
            this.props.initialValues.location.type = String(this.props.initialValues.location.type);
        }
        
        const photoUrl = this.props.eventId ?
            `api/photo/GetFullEventPhoto?id=${this.props.eventId}` : null;

        return (
            <form onSubmit={this.props.handleSubmit(this.props.onSubmit)}
                encType="multipart/form-data" autoComplete="off">
                <div className="text text-2 pl-md-4 pt-md-2">
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
                            <Field
                                type="checkbox"
                                label="Recurrent Event"
                                name='isReccurent'
                                component={renderCheckbox}
                                checked={checked}
                                onChange={this.handleChange} />
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
                    <div className="meta-wrap">
                        <span >
                            <Field
                                name='dateFrom'
                                label='From'
                                component={renderDatePicker}
                            />
                        </span>
                        {form_values && form_values.dateFrom &&
                            <span className="retreat">
                                <Field
                                    name='dateTo'
                                    label='To'
                                    minValue={form_values.dateFrom}
                                    component={renderDatePicker}
                                />
                            </span>
                        }
                    </div>
                    <div className="mt-3">
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
                    <Field name="location.type" component={radioLocationType} />
                    {this.props.form_values
                        && this.props.form_values.location
                        && this.props.form_values.location.type == enumLocationType.map &&

                        <div className="mt-2">
                            <Field
                                name='location'
                                component={renderLocationMapWithMarker}
                            />
                        </div>
                    }
                    {this.props.form_values
                        && this.props.form_values.location
                        && this.props.form_values.location.type == enumLocationType.online &&

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
                </div>
                <div className="row pl-md-4 my-4">
                    {this.props.children}
                </div>
            </form>
        );
    }
}

export default reduxForm({
    form: 'event-form',
    enableReinitialize: true
})(EventForm);
