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
    state = { imagefile: [], checked: false, photoCropped: false };

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

    setCroppedImage = (croppedImage, onChange) => {
        const file = new File([croppedImage], "image.jpg", { type: "image/jpeg" });
        const imagefile = {
            file: file,
            name: "image.jpg",
            preview: croppedImage,
            size: 1
        };
        this.setState({ imagefile: [imagefile], photoCropped: true }, () => onChange(imagefile));
    }

    componentWillUnmount() {
        this.resetForm();
    }

    handleChange = () => {
        this.setState(state => ({
            checked: !state.checked,
        }));
    }

    resetForm = () => {
        this.setState({ photoCropped: false });
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

        const { countries, form_values, all_categories, data, isCreated, pristine, invalid, submitting } = this.props;
        const { photoCropped } = this.state;
        let values = form_values || this.props.initialValues;

        return (
            <form onSubmit={this.props.handleSubmit} encType="multipart/form-data" autoComplete="off" >
                <div className="text text-2 pl-md-4">
                    <Field
                        id="image-field"
                        name="image"
                        component={DropZoneField}
                        type="file"
                        imagefile={this.state.imagefile}
                        handleOnDrop={this.handleOnDrop}
                        handleOnCrop={this.setCroppedImage}
                        crop={true}
                        cropShape='rect'
                        handleOnClear={this.resetForm}
                        validate={(this.state.imagefile[0] == null) ? [imageIsRequired] : null}
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
                            name='countryId'
                            data={countries}
                            text='Country'
                            component={renderSelectLocationField}
                        />
                    </div>
                    {values && values.countryId &&
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
                <div className="row pl-md-4">
                    <div className="col">
                        <Button
                            className="border"
                            fullWidth={true}
                            type="submit"
                            color="primary"
                            disabled={!photoCropped || pristine || submitting}>
                            Save
                        </Button>
                    </div>
                    <div className="col">
                        <Button
                            className="border"
                            fullWidth={true}
                            color="primary"
                            onClick={this.props.onCancel}>
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