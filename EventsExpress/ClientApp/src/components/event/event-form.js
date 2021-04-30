
import React, { Component } from 'react';
import { reduxForm, Field } from 'redux-form';
import { connect } from 'react-redux';
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
    renderDatePicker,
    radioLocationType
} from '../helpers/helpers';
import Inventory from '../inventory/inventory';
import LocationMap from './map/location-map';
import { enumLocationType } from '../../constants/EventLocationType';
import { createBrowserHistory } from 'history';
import "./event-form.css";
momentLocaliser(moment);
const history = createBrowserHistory({ forceRefresh: true });

class EventForm extends Component {

    state = { checked: false };

    handleChange = () => {
        this.setState(state => ({
            checked: !state.checked,
        }));

    }

    onClickCallBack = (coords) => {
        this.setState({ selectedPos: [coords.lat, coords.lng] });
    }

    render() {
        const { form_values, all_categories, isCreated, disabledDate, initialValues } = this.props;
        const { checked } = this.state;
        const { handleChange } = this;

        let values = form_values || initialValues;
        const photoUrl = initialValues ?
            initialValues.photoUrl : null;

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
                    <div className="meta-wrap">
                        <span >
                            <Field
                                name='dateFrom'
                                label='From'
                                component={renderDatePicker}
                                disabled={disabledDate ? true : false}
                            />
                        </span>
                        {values && values.dateFrom &&
                            <span className="retreat">
                                <Field
                                    name='dateTo'
                                    label='To'
                                    minValue={values.dateFrom}
                                    component={renderDatePicker}
                                    disabled={disabledDate ? true : false}
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
                                    this.props.initialValues.location &&
                                    this.props.initialValues.location.selectedPos
                                }
                                initialValues={initialValues}
                                isAddEventMapLocation={true}
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
                                className="mb-4"
                            />
                        </div>
                    }
                    {isCreated ? null : <Inventory />}
                </div>
                <div className="row pl-md-4 mb-4">
                    {this.props.children}
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
    enableReinitialize: true
})(EventForm);
