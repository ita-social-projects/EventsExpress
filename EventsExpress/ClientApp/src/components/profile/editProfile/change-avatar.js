import React from 'react';
import DropZoneField from '../../helpers/DropZoneField';
import { reduxForm, Field } from 'redux-form';
import Button from "@material-ui/core/Button";

const enhanceWithPreview = files =>
  files.map(file =>
    Object.assign({}, file, {
      preview: URL.createObjectURL(file),
    })
  )

  const imageIsRequired = value => (!value ? "Required" : undefined);



class ChangeAvatar extends React.Component {
    
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

    render(){

    const { handleSubmit, pristine, reset, submitting } = this.props;

    return (
        <form onSubmit={handleSubmit}>
                    <Field
          name="image"
          component={DropZoneField}
          type="file"
          imagefile={this.state.imagefile}
          handleOnDrop={this.handleOnDrop}
          validate={[imageIsRequired]}
        />        
            <Button
                type="button" color="primary"
        disabled={this.props.pristine || this.props.submitting}
        onClick={this.resetForm}
        style={{ float: "right" }}
      >
        Clear
      </Button>

            <div>
                <Button  color="primary" type="submit" disabled={pristine || submitting}>
                    Submit
                </Button >
            </div>
        </form>
    );
    }
};

export default reduxForm({
    form: "change-avatar" // a unique identifier for this form
})(ChangeAvatar);
