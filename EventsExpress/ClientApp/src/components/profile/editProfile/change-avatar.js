import React from 'react';
import DropZoneField from '../../helpers/DropZoneField';
import { reduxForm, Field } from 'redux-form';
import Button from "@material-ui/core/Button";
import { connect } from 'react-redux';

const required = value => value ? undefined : "Required";

let ChangeAvatar = props => {
  const { handleSubmit, pristine, submitting } = props;

  return (
    <form onSubmit={handleSubmit}>
      <Field
        name="image"
        component={DropZoneField}
        type="file"
        crop={true}
        cropShape='round'
        validate={[required]}
      />
      <div>
        <Button color="primary" type="submit" disabled={pristine || submitting}>
          Submit
        </Button >
      </div>
    </form>
  );
}

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
  form: "change-avatar"
})(ChangeAvatar);