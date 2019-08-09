import React, { Component } from 'react';
import { reduxForm, Field } from 'redux-form';
import { renderTextField, renderDatePicker } from '../helpers/helpers';
import './event-form.css';
import Button from "@material-ui/core/Button";
import 'react-widgets/dist/css/react-widgets.css'
import moment, { now } from 'moment';
import momentLocaliser from 'react-widgets-moment';
import DropZoneField from '../helpers/DropZoneField';
import Module from '../helpers';
import { renderMultiselect, renderSelectLocationField } from '../helpers/helpers';
import { connect } from 'react-redux';

momentLocaliser(moment)
const enhanceWithPreview = files =>
  files.map(file =>
    Object.assign({}, file, {
      preview: URL.createObjectURL(file),
    })
  )

const imageIsRequired = value => (!value ? "Required" : undefined);

const { validate } = Module;

class EventForm extends Component {

  state = { imagefile: [] };

  handleFile(fieldName, event) {
    event.preventDefault();
    // convert files to an array
    const files = [...event.target.files];
  }
  handleOnDrop = (newImageFile, onChange) => {
    const imagefile = {
      file: newImageFile[0],
      name: newImageFile[0].name,
      preview: URL.createObjectURL(newImageFile[0]),
      size: newImageFile[0].size
    };
    this.setState({ imagefile: [imagefile] }, () => onChange(imagefile));
  };

  componentDidMount = () => {

    let values = this.props.form_values || this.props.initialValues;

    if (this.props.isCreated) {
      const imagefile = {
        file: '',
        name: '',
        preview: values.photoUrl,
        size: ''
      };
      this.setState({ imagefile: [imagefile] });
    }
  }

  componentWillUnmount() {
    this.resetForm();
  }

  resetForm = () => this.setState({ imagefile: [] });

  renderLocations = (arr) => {
    return arr.map((item) => {

      return <option value={item.id}>{item.name}</option>;
    });
  }

  render() {

    const { countries, form_values, all_categories, data } = this.props;
    let values = form_values || this.props.initialValues;
    let countries_list = this.renderLocations(countries);
    if(this.props.Event.isEventSuccess){
    this.resetForm();
    }

    return (
      <form onSubmit={this.props.handleSubmit} encType="multipart/form-data">
        <div className="text text-2 pl-md-4">
          <Field
          ref={(x) => {this.image = x; }}
            id="image-field"
            name="image"
            component={DropZoneField}
            type="file"
            imagefile={this.state.imagefile}
            handleOnDrop={this.handleOnDrop}

            validate={(this.state.imagefile[0] == null) ? [imageIsRequired] : null}
          />
          <button
            type="button"
            className="uk-button uk-button-default uk-button-large clear"
            disabled={this.props.submitting}
            onClick={this.resetForm}
            style={{ float: "right" }}
          >
            Clear
                          </button>

          <div className="mt-2">
            <Field name='title' component={renderTextField} defaultValue={data.title} type="input" label="Title" />
          </div>
          <div className="meta-wrap m-2">
            <p className="meta">
              <span>From<Field name='dateFrom' component={renderDatePicker} /></span>
              {values.dateFrom != null &&
                <span>To<Field name='dateTo' defaultValue={values.dateFrom} minValue={values.dateFrom} component={renderDatePicker} /></span>
              }
            </p>
          </div>

          <div className="mt-2">
            <Field name='description' component={renderTextField} type="input" label="Description" />
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
              component={renderSelectLocationField} />
          </div>
          {values.countryId != null &&
            <div className="mt-2">
              <Field name='cityId' data={this.props.cities} text='City' component={renderSelectLocationField} />
            </div>
          }
        </div>


        <Button fullWidth={true} type="submit" value="Login" color="primary" disabled={this.props.submitting}>
          Save
                </Button>
      </form>
    );
  }
}

const mapStateToProps = (state) => ({
  initialValues: state.event.data
});

const mapDispatchToProps = (dispatch) => {
  return {

  }
};

const asd = reduxForm({
  form: 'event-form',
  validate: validate,
  enableReinitialize: true
})(EventForm);
const qwe = connect(mapStateToProps, mapDispatchToProps)(asd);



export default qwe;
