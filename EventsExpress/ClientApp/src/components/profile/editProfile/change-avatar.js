import React from 'react';
import DropZoneField from '../../helpers/DropZoneField';
import { reduxForm, Field } from 'redux-form';
import Button from "@material-ui/core/Button";
import Module from '../../helpers';

const { validate } = Module;

let ChangeAvatar = props => {
  const { handleSubmit, submitting } = props;

  return (
    <form onSubmit={handleSubmit}>
      <Field
        name="image"
        component={DropZoneField}
        type="file"
        crop={true}
        cropShape='round'
        photoUrl={props.initialValues.image}
      />
      <div>
        <Button color="primary" type="submit" disabled={submitting}>
          Submit
        </Button >
      </div>
    </form>
  );
}

export default reduxForm({
  form: "change-avatar",
  validate: validate
})(ChangeAvatar);