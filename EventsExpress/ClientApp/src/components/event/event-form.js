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
import { renderMultiselect } from '../helpers/helpers';
import {connect } from 'react-redux';
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

  resetForm = () => this.setState({ imagefile: [] }, () => this.props.reset());

  renderLocations = (arr) => {
    return arr.map((item) => {

      return <option value={item.id}>{item.name}</option>;
    });
  }

  renderLocations = (arr) => {
    return arr.map((item) => {
  
      return <option value={item.id}>{item.name}</option>;
    });
  }

  render() {

    const { countries, form_values, all_categories, data } = this.props;
    let values = form_values || this.props.initialValues;
    let countries_list = this.renderLocations(countries);

    return (
      <form onSubmit={this.props.handleSubmit} encType="multipart/form-data">
        <div className="text text-2 pl-md-4">
          <Field
            name="image"
            component={DropZoneField}
            type="file"
            imagefile={this.state.imagefile}
            handleOnDrop={this.handleOnDrop}
            // validate={[imageIsRequired]}
          />
          <button
            type="button"
            className="uk-button uk-button-default uk-button-large clear"
            disabled={this.props.pristine || this.props.submitting}
            onClick={this.resetForm}
            style={{ float: "right" }}
          >
            Clear
                          </button>
          <Field name='title' component={renderTextField} defaultValue={data.title} type="input" label="Title" />
          <div className="meta-wrap m-2">
            <p className="meta">
              <span>From<Field name='dateFrom' component={renderDatePicker} /></span>
              {values.dateFrom != null &&
                <span>To<Field name='dateTo' defaultValue={values.dateFrom} minValue={values.dateFrom} component={renderDatePicker} /></span>
              }
            </p>
          </div>

          <Field name='description' component={renderTextField} type="input" label="Description" />

          <Field
            name="categories"
            component={renderMultiselect}
            data={all_categories.data}
            valueField={"id"}
            textField={"name"}
            className="form-control mt-2"
            placeholder='#hashtags'
          />
          <Field onChange={this.props.onChangeCountry} className="form-control mt-2" name='country' component="select">
            <option>Country</option>
            {countries_list}
          </Field>
          {values.country != null &&
            <Field name='city' className="form-control mt-2" component="select">
              <option>City</option>
              {this.renderLocations(this.props.cities)}
            </Field>
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
  enableReinitialize: true,
  // initialValues: {}
})(EventForm);
const qwe = connect(mapStateToProps, mapDispatchToProps)(asd);



export default qwe;
