import React, { Component } from 'react';
import { reduxForm, Field } from 'redux-form';
import { renderTextField } from '../helpers/helpers';
import './event-form.css';
import Button from "@material-ui/core/Button";

import Dropzone from 'react-dropzone';

import DropZoneField from '../helpers/DropZoneField';

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

    onChangePhoto = values => {
        var file = document.getElementById('imgupload');
        console.log(file.files);
        console.log(values);
        // var formData = new FormData(file);
        // console.log(file.files[0]);
        // formData.append('formFile', file.files[0]);
        // console.log(formData);
        
    }

    onClickChangePhoto = (e) =>{
        e.preventDefault();
        document.getElementById('imgupload').click();
    }

    handleFile(fieldName, event) {
        event.preventDefault();
        // convert files to an array
        const files = [ ...event.target.files ];
        console.log(files);
    }
    handleOnDrop = (newImageFile, onChange) => {
        const imagefile = {
          file: newImageFile[0],
          name: newImageFile[0].name,
          preview: URL.createObjectURL(newImageFile[0]),
          size: newImageFile[0].size
        };
        console.log(imagefile);
        this.setState({ imagefile: [imagefile] }, () => onChange(imagefile));
        console.log(this.state);
      };
    
      resetForm = () => this.setState({ imagefile: [] }, () => this.props.reset());

    render() {

        return (
            <form onSubmit={ this.props.handleSubmit } encType="multipart/form-data">
 
                    {/* <Field name="image" type="file"  label="change" component="input" onChange={this.handleFile.bind(this, 'image')}s /> */}
                    
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
                            <div className="meta-wrap">
                                <p className="meta">    
                                <span><i className="fa fa-calendar mr-2"></i>
                        <Field name='date_from' component="input" type="date" label="" /></span>
                                <span><a href="single.html"><i className="fa fa-folder mr-2"></i>Travel</a></span>
                                <span><i className="fa fa-comment mr-2"></i>2 Comment</span>
                                </p>
                            </div>
                            
                        <Field name='description' component={renderTextField} type="input" label="Description" />
                            <p><a href="#" className="btn-custom">Read More <span className="ion-ios-arrow-forward"></span></a></p>
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
 