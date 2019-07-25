import React, { Component } from 'react';
import { reduxForm, Field } from 'redux-form';
import { renderTextField } from '../helpers/helpers';
import './event-form.css';
import Button from "@material-ui/core/Button";
import 'react-widgets/dist/css/react-widgets.css'
import {renderDateTimePicker } from '../helpers/helpers';
import DropdownList from 'react-widgets/lib/DropdownList';
import moment from 'moment';
import momentLocaliser from 'react-widgets-moment';
import DropZoneField from '../helpers/DropZoneField';
momentLocaliser(moment)
const enhanceWithPreview = files =>
  files.map(file =>
    Object.assign({}, file, {
      preview: URL.createObjectURL(file),
    })
  )

const withPreviews = dropHandler => (accepted, rejected) =>
  dropHandler(enhanceWithPreview(accepted), rejected)

  const imageIsRequired = value => (!value ? "Required" : undefined);




export class EventForm extends Component {

    state = { imagefile: [] };
    
    handleFile(fieldName, event) {
        event.preventDefault();
        // convert files to an array
        const files = [ ...event.target.files ];
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

      renderLocations = (arr) =>{
        return arr.map((item) => {
            
          return <option value={item.id}>{item.name}</option>;
        });
      }

    render() {

        const { countries, form_values } = this.props;

        let countries_list = this.renderLocations(countries);

        return (
            <form onSubmit={ this.props.handleSubmit } encType="multipart/form-data">
 
                    <Field
          name="image"
          component={DropZoneField}
          type="file"
          imagefile={this.state.imagefile}
          handleOnDrop={this.handleOnDrop}
          validate={[imageIsRequired]}
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

                    <div className="text text-2 pl-md-4">
                        <Field name='title' component={renderTextField} type="input" label="Title" />
                            <div className="meta-wrap m-2">
                                <p className="meta">    
                                <span>From
                                         <Field name='date_from' component="input" type="date" label="" /></span>
                                <span>To<Field name='date_to' component="input" type="date" label="" /></span>
                                </p>
                            </div>
                            
                        <Field name='description' component={renderTextField} type="input" label="Description" />
                           
                        <Field onChange={this.props.onChangeCountry} className="form-control" name='country' component="select">
                            <option>Country</option>
                            {countries_list}
                        </Field>
                    {form_values &&
                    <Field name='city' className="form-control" component="select">
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

EventForm = reduxForm({
    form: 'event-form'
})(EventForm);

export default EventForm;
 