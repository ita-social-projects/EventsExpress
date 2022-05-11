import React from "react";
import DropZoneField from "../../helpers/DropZoneField";
import { reduxForm, Field } from "redux-form";
import Button from "@material-ui/core/Button";
import PhotoService from "../../../services/PhotoService";
import { UserImageResizer } from "../../helpers/image-resizer";

const validate = (values) => {
  const errors = {};
  if (values.image === null || values.image === undefined) {
    errors.image = "Image is required";
  }

  return errors;
};

const photoService = new PhotoService();

let ChangeAvatar = (props) => {
  const { handleSubmit, pristine, submitting, invalid } = props;

  return (
    <form name="change-avatar" onSubmit={handleSubmit}>
      <Field
        name="image"
        component={DropZoneField}
        type="file"
        crop={true}
        cropShape="round"
        loadImage={() => photoService.getUserPhoto(props.initialValues.userId)}
        imageResizer={UserImageResizer}
      />
      <div>
        <Button
          color="primary"
          type="submit"
          style={{top: "2px"}}
          disabled={pristine || submitting || invalid}
        >
          Submit
        </Button>
      </div>
    </form>
  );
};

export default reduxForm({
  form: "change-avatar",
  enableReinitialize: true,
  validate,
})(ChangeAvatar);
