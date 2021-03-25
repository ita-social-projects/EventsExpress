import React from 'react';
import DropZoneField from '../../helpers/DropZoneField';
import { reduxForm, Field } from 'redux-form';
import Button from "@material-ui/core/Button";
import Module from '../../helpers';
import ErrorMessages from '../../shared/errorMessage';

const { validate } = Module;

let ChangeAvatar = props => {
    const { handleSubmit, pristine, submitting } = props;

    return (
        <form name="change-avatar" onSubmit={handleSubmit}>
            <Field
                name="image"
                component={DropZoneField}
                type="file"
                crop={true}
                cropShape='round'
                photoUrl={props.initialValues.image}
            />
            {
                props.error &&
                <ErrorMessages error={props.error} className="text-center" />
            }
            <div>
                <Button color="primary" type="submit" disabled={pristine || submitting}> Submit </Button >
            </div>
        </form>
    )    
};

export default reduxForm({
    form: "change-avatar",
    validate: validate
})(ChangeAvatar);