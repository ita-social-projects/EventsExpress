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
    renderDatePicker
} from '../helpers/helpers';
import Inventory from '../inventory/inventory';
import LocationMap from './map/location-map';
import Geolocation from 'react-native-geolocation-service';
import Geocoder from 'react-native-geocoding'; 
import L from 'leaflet'

momentLocaliser(moment);
const imageIsRequired = value => (!value ? "Required" : undefined);
const { validate } = Module;

class EventForm extends Component {
    constructor(props) {
        super(props);
        this.state = { 
            imagefile: [],
            checked: false,
            position: [50.4547, 30.5238],
            selectedPos: null
        };
        this.callbackFunction = this.callbackFunction.bind(this);
    }

    getUserGeolocation = () => {
        Geolocation.getCurrentPosition((position) => {
            this.setState
            ({
                position: [position.coords.latitude, position.coords.longitude]
            });
        }, (error) => {
            console.log(error.code, error.message);
        }, 
        {
            enableHighAccuracy: false,
            timeout: 10000,
            maximumAge: 100000
        });
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

    callbackFunction = (childData) => {
        console.log("childData", childData);
        this.setState({selectedPos: childData})
    }

    componentDidMount = () => {
        this.getUserGeolocation();
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

    componentWillMount() {
        this.resetForm();
    }

    render() {

        const { form_values, all_categories, isCreated } = this.props;
        var position = L.latLng(this.state.position);
        var selectedPos = this.state.selectedPos || position;
        let values = form_values || this.props.initialValues;
        console.log("form", selectedPos.lat);

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
                        <LocationMap 
                            position={this.state.position} 
                            parentCallback = {this.callbackFunction}/>
                    </div>
                    <div className="mt-2">
                        <Field
                            name="selectedPos"
                            component={renderTextField}
                            placeholder={selectedPos}
                            defaultValue={selectedPos}
                            hidden={true}/>
                    </div>
                    {isCreated ? null : <Inventory />}
                </div>
                <div className="row pl-md-4">
                    <div className="col">
                        <Button 
                            className="border"
                            fullWidth={true}
                            type="submit"
                            color="primary"
                            onClick={this.disableSaveButton}
                            disabled={this.isSaveButtonDisabled}>
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