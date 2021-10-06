import React, { Component } from 'react';
import { reduxForm, Field, change } from 'redux-form';
import moment from 'moment';
import 'react-widgets/dist/css/react-widgets.css'
import momentLocaliser from 'react-widgets-moment';
import DropZoneField from '../helpers/DropZoneField';
import PhotoService from '../../services/PhotoService';
import periodicity from '../../constants/PeriodicityConstants'
import {
    renderDatePicker, LocationMapWithMarker, renderCheckbox, radioButton,
    renderSelectField, renderTextField, renderTextArea, renderMultiselect
} from '../helpers/form-helpers';
import { enumLocationType } from '../../constants/EventLocationType';
import "./event-form.css";
import FormControlLabel from '@material-ui/core/FormControlLabel';
import Radio from '@material-ui/core/Radio';
import asyncValidatePhoto from '../../containers/async-validate-photo';
import MultieventForm from './MultieventForm'

momentLocaliser(moment);

const photoService = new PhotoService();

class EventForm extends Component {

    
    checkLocation = (location) => {

        if (location !== null) {
            if (location.type == enumLocationType.map) {
                location.latitude = null;
                location.longitude = null;
                change(`event-form`, `location`, location);
            }

            if (location.type == enumLocationType.online) {
                location.onlineMeeting = null;
                change(`event-form`, `location.onlineMeeting`, location);
            }
        }
    }

    periodicityListOptions = (periodicity.map((item) =>
        <option value={item.value} key={item.value}> {item.label} </option>
    ));


    render() {

        const { form_values, all_categories, disabledDate, user_name } = this.props;
   
        return (
            <form onSubmit={this.props.handleSubmit(this.props.onSubmit)}
                encType="multipart/form-data" autoComplete="off">
                <div className="text text-2 pt-md-2">
                    <Field
                        id="image-field"
                        name="photo"
                        component={DropZoneField}
                        type="file"
                        crop={true}
                        cropShape='rect'
                        loadImage={() => photoService.getFullEventPhoto(this.props.eventId)}
                    />
                    <div className="mt-2">
                        <Field
                            name='organizer'
                            component={renderTextField}
                            type='input'
                            label='Organizer'
                            inputProps={{ value: user_name }}
                            readOnly
                        />
                    </div>
                    <div className="mt-2">
                        <Field
                            name='title'
                            component={renderTextField}
                            type="input"
                            label="Title"
                            inputProps={{ maxLength: 60 }}
                        />
                    </div>
                    <div className="mt-2">
                        <Field parse={Number}
                            name='maxParticipants'
                            component={renderTextField}
                            type="number"
                            label="Max Count Of Participants"
                        />
                    </div>
                    {this.props.haveReccurentCheckBox &&
                        <div className="mt-2">
                        <Field
                            name='isReccurent'
                            component={renderCheckbox}
                            type="checkbox"
                            label="IsReccurent"
                        />
                        </div>

                    }
                    {this.props.haveReccurentCheckBox && form_values && form_values.isReccurent &&
                        <div>
                            <div className="mt-2">
                            <Field
                                    minWidth={200}
                                    name="periodicity"
                                    text="Periodicity"
                                    component={renderSelectField}
                                >
                                {this.periodicityListOptions}
                                </Field>
                            </div>
                            <div className="mt-2">
                                <Field
                                    name='frequency'
                                    type="number"
                                    component={renderTextField}
                                    label="Frequency"
                                />
                            </div>
                        </div>
                    }
                    <div className="mt-2">
                        <Field
                            name="isPublic"
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
                                disabled={disabledDate}
                                component={renderDatePicker}
                            />
                        </span>
                        {form_values && form_values.dateFrom &&
                            <span className="retreat">
                                <Field
                                    name='dateTo'
                                    label='To'
                                    disabled={disabledDate}
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
                            className="form-control"
                            placeholder='#hashtags'
                        />
                    </div>
                    <Field name="location.type" component={radioButton} parse={Number} onChange={() => this.checkLocation(this.props.form_values.location)}>
                        <FormControlLabel value={0} control={<Radio />} label="Map" />
                        <FormControlLabel value={1} control={<Radio />} label="Online" />
                    </Field>
                        {this.props.form_values
                            && this.props.form_values.location
                            && this.props.form_values.location.type == enumLocationType.map &&

                            <div className="mt-2">
                                <Field
                                    name='location'
                                    component={LocationMapWithMarker}
                                />
                            </div>
                        }
                        {this.props.form_values
                            && this.props.form_values.location
                            && this.props.form_values.location.type == enumLocationType.online &&

                            <div className="mt-2">
                                <label htmlFor="url">Enter an https:// URL:</label>
                                <Field
                                    name='location.onlineMeeting'
                                    component={renderTextField}
                                    type="url"
                                    label="Url"
                                    id="url"
                                />
                            </div>
                    }
                    <div className="mt-2">
                        <Field
                            type="checkbox"
                            label="Multi Event"
                            name='isMultiEvent'
                            component={renderCheckbox}
                        />
                       
                    </div>
                    {this.props.form_values
                        && this.props.form_values.isMultiEvent &&
                                

                            <div className="mt-2">
                                <Field
                            name='location.MultiEvent'
                            component={MultieventForm}
                            form_values={form_values }
                                />
                            </div>
                         
                    }
                    
                </div>
                <div className="row my-4">
                    {this.props.children}
                </div>
            </form>
        );
    }
}

export default reduxForm({
    form: 'event-form',
    enableReinitialize: true,
    touchOnChange: true,
    asyncValidate: asyncValidatePhoto,
    asyncChangeFields: ['photo']
})(EventForm);
