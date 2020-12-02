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
    renderSelectLocationField,
    renderTextArea,
    renderSelectPeriodicityField,
    renderCheckbox,
    renderTextField,
    renderDatePicker
} from '../helpers/helpers';
import Inventory from '../inventory/inventory';

momentLocaliser(moment);

const imageIsRequired = value => (!value ? "Required" : undefined);
const { validate } = Module;

class EventForm extends Component {
    state = { imagefile: [], checked: false };

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
        let values = this.props.initialValues || this.props.data;

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

    componentDidUpdate = () => {
        let status = this.props.addEventStatus;
        if (status && status.isEventSuccess) {
            this.resetForm();
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

    handleChange = () => {
        this.setState(state => ({
            checked: !state.checked,
        }));
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

    componentWillMount() {
        this.resetForm();
    }

    render() {

        const { countries, form_values, all_categories, data, isCreated } = this.props;
        let values = form_values || this.props.initialValues;
        
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
                            type="input"
                            label="Title"
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
                                label="Reccurent Event"
                                name='isReccurent'
                                component={renderCheckbox}
                                checked={this.state.checked}
                                onChange={this.handleChange} />
                        </div>
                    }
                    {this.state.checked &&
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
                                disabled={this.props.disabledDate ? true : false}
                            />
                        </span>
                        {values && values.dateFrom &&
                            <span>To
                                <Field
                                    name='dateTo'
                                    minValue={values.dateFrom}
                                    component={renderDatePicker}
                                    disabled={this.props.disabledDate ? true : false}
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
                    <div className="mt-2">
                        <Field onChange={this.props.onChangeCountry}
                            name='country.id'
                            data={countries}
                            text='Country'
                            component={renderSelectLocationField}
                        />
                    </div>
                    {values && values.country.id  &&
                        <div className="mt-2">
                            <Field
                                name='city.id'
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