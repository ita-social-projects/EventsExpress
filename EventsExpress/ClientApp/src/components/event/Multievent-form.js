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

momentLocaliser(moment);

class MultiEventForm extends Component {

    state = { checked: false };

    handleChange = () => {
        this.setState(state => ({
            checked: !state.checked,
        }));
    }

    

    checkLocation = (location) => {
        if (location) {
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

    render() {
        //const { form_values, all_categories, disabledDate } = this.props;
        //const { checked } = this.state;

        //if (this.props.initialValues.location != null) {
        //    this.props.initialValues.location.type = String(this.props.initialValues.location.type);
        //}

        return (
            <form onSubmit={this.props.handleSubmit(this.props.onSubmit)}
                encType="multipart/form-data" autoComplete="off">
                <div className="text text-2 pt-md-2">
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
                    </div>

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

                </div>
                <div className="row my-4">
                    {this.props.children}
                </div>
            </form>
              
    
        );
    }
}

export default reduxForm({
    form: 'Multievent-form',
    enableReinitialize: true
})(MultiEventForm);
