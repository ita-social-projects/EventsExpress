import React from 'react';
import DropZoneField from '../../helpers/DropZoneField';
import { reduxForm, Field } from 'redux-form';
import Button from "@material-ui/core/Button";
import ErrorMessages from '../../shared/errorMessage';


 const validate = values => {
    const errors = {};
     if (values.image != null && values.image.file != null && values.image.file.size < 4096) { errors.image = "Image is too small"; }
     if (values.image === null || values.image === undefined) { errors.image = "Image is required"; }

    return errors;
}


let ChangeAvatar = props => {
    const { handleSubmit, pristine, submitting, invalid } = props;
    

    return (
        <form name="change-avatar" onSubmit={handleSubmit}>
            <Field
                name="image"
                component={DropZoneField}
                type="file"
                crop={true}
                cropShape='round'
                photoUrl={`api/photo/GetUserPhoto?id=${props.initialValues.userId}`}
            />
            {
                props.error &&
                <ErrorMessages error={props.error} className="text-center" />
            }
            <div>
                <Button color="primary" type="submit" disabled={pristine || submitting || invalid}> Submit </Button >
            </div>
        </form>
    )    
};

export default reduxForm({
    form: "change-avatar",
    enableReinitialize: true,
    validate
})(ChangeAvatar);