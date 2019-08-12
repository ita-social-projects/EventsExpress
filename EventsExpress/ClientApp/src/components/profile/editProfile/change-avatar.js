import React from 'react';
import DropZoneField from '../../helpers/DropZoneField';
import { reduxForm, Field } from 'redux-form';
import { connect } from 'react-redux';


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
          size: 1
        };
        this.setState({ imagefile: [imagefile] }, () => onChange(imagefile));
      };
    
      resetForm = () => this.setState({ imagefile: [] }, () => this.props.reset());

      componentWillMount = () => {
          const imagefile = {
            file: '',
            name: '',
            preview: this.props.current_photo,
            size: 1
          };
          this.setState({ imagefile: [imagefile] });
        }

    componentWillUnmount = () => {
      this.resetForm();
    }

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

            <div>
                <button type="submit" disabled={pristine || submitting}>
                    Submit
                </button>
            </div>
        </form>
    );
    }
};


const mapStateToProps = (state) => ({
  current_photo: state.user.photoUrl
})

const mapDispatchToProps = dispatch => {
  return {
  };
}

ChangeAvatar = connect(
  mapStateToProps,
  mapDispatchToProps
)(ChangeAvatar);

export default reduxForm({
    form: "change-avatar" // a unique identifier for this form
})(ChangeAvatar);
