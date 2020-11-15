import React, { Component } from 'react';
import { reduxForm, Field } from 'redux-form';
import { connect } from 'react-redux';
import moment from 'moment';
import Button from "@material-ui/core/Button";
import { renderTextField, renderDatePicker } from '../helpers/helpers';
import 'react-widgets/dist/css/react-widgets.css'
import momentLocaliser from 'react-widgets-moment';
import DropZoneField from '../helpers/DropZoneField';
import Module from '../helpers';
import {
    renderMultiselect,
    renderSelectLocationField,
    renderTextArea
} from '../helpers/helpers';
import './event-form.css';
import Inventory from '../inventory/inventory';

momentLocaliser(moment);

const imageIsRequired = value => (!value ? "Required" : undefined);
const { validate } = Module;

class EventForm extends Component {
    state = { imagefile: [] };

    handleFile(fieldName, event) {
        event.preventDefault();
        const files = [...event.target.files];
    }

    handleOnDrop = (newImageFile, onChange) => {
        if (newImageFile.length > 0) {
            const imagefile = {
                file: newImageFile[0],
                name: newImageFile[0].name,
                preview: URL.createObjectURL(newImageFile[0]),
                size: 1
            };
            this.setState({ imagefile: [imagefile] }, () => onChange(imagefile));
        }
    };

    componentDidMount = () => {
        let values = this.props.form_values || this.props.initialValues;

        if (this.props.isCreated) {
            const imagefile = {
                file: '',
                name: '',
                preview: values.photoUrl,
                size: 1
            };
            this.setState({ imagefile: [imagefile] });
        }
    }

    componentWillUnmount() {
        this.resetForm();
    }

    isSaveButtonDisabled = false;

    disableSaveButton = () => {
        if (this.props.valid) {
            this.isSaveButtonDisabled = true;
        }
    }

    resetForm = () => {
        this.isSaveButtonDisabled = false;
        this.setState({ imagefile: [] });
    }

    renderLocations = (arr) => {
        return arr.map((item) => {
            return <option value={item.id}>{item.name}</option>;
        });
    }

    render() {
        const { countries, form_values, all_categories, data, isCreated } = this.props;
        let values = form_values || this.props.initialValues;

        if (this.props.Event.isEventSuccess) {
            this.resetForm();
        }

        return (
            <form onSubmit={this.props.handleSubmit} encType="multipart/form-data" autoComplete="off" >
                <div className="text text-2 pl-md-4">
                    <Field
                        ref={(x) => { this.image = x; }}
                        id="image-field"
                        name="image"
                        component={DropZoneField}
                        type="file"
                        imagefile={this.state.imagefile}
                        handleOnDrop={this.handleOnDrop}
                        validate={(this.state.imagefile[0] == null) ? [imageIsRequired] : null}
                    />
                    <Button
                        type="button"
                        color="primary"
                        disabled={this.props.submitting}
                        onClick={this.resetForm}
                        style={{ float: "right" }}
                    >
                        Clear
                    </Button>
                    <div className="mt-2">
                        <Field name='title'
                            component={renderTextField}
                            defaultValue={data.title}
                            type="input"
                            label="Title"
                        />
                    </div>
                    <div className="mt-2">
                        <Field
                            name='maxParticipants'
                            component={renderTextField}
                            defaultValue={data.maxParticipants}
                            type="number"
                            label="Max Count Of Participants"
                        />
                    </div>
                    <div className="meta-wrap m-2">
                        <span>From
                            <Field
                                name='dateFrom'
                                component={renderDatePicker}
                            />
                        </span>
                        {values.dateFrom != null &&
                            <span>To
                                <Field
                                    name='dateTo'
                                    defaultValue={values.dateFrom}
                                    minValue={values.dateFrom}
                                    component={renderDatePicker}
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
                            placeholder='#hashtags'
                        />
                    </div>
                    <div className="mt-2">
                        <Field onChange={this.props.onChangeCountry}
                            name='countryId'
                            data={countries}
                            text='Country'
                            component={renderSelectLocationField}
                        />
                    </div>
                    {values.countryId != null &&
                        <div className="mt-2">
                            <Field
                                name='cityId'
                                data={this.props.cities}
                                text='City'
                                component={renderSelectLocationField}
                            />
                        </div>
                    }
                    {isCreated ? null : <Inventory />}
                </div>
                <Button
                    fullWidth={true}
                    type="submit"
                    color="primary"
                    onClick={this.disableSaveButton}
                    disabled={this.isSaveButtonDisabled}
                >
                    Save
                </Button>
            </form>
        );
    }
}

const mapStateToProps = (state) => ({
    initialValues: state.event.data
});

EventForm = connect(
    mapStateToProps
)(EventForm);

export default reduxForm({
    form: 'event-form',
    validate: validate,
    enableReinitialize: true
})(EventForm);