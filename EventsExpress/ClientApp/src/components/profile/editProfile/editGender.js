import React from "react";
import { Field, reduxForm } from "redux-form";
import Button from "@material-ui/core/Button";
import { renderSelectField } from '../../helpers/form-helpers'
import ErrorMessages from '../../shared/errorMessage';
import IconButton from "@material-ui/core/IconButton";

let EditGender = props => {
    const { handleSubmit, pristine, submitting, onClose } = props;
    return (
        <form name= "EditGender" onSubmit={handleSubmit}>
            <div>
                <Field
                    minWidth={210}
                    name="gender"
                    component={renderSelectField}
                    label="Gender"
                >
                    <option aria-label="None" value="" />
                    <option value="1">Male</option>
                    <option value="2">Female</option>
                    <option value="3">Other</option>
                </Field>
                {
                    props.error &&
                    <ErrorMessages error={props.error} className="text-center" />
                }
            </div>
            
            <div className='editFieldButtons'>
                {/* <Button type="submit" color="primary" disabled={pristine || submitting}>
                    Submit
                </Button> */}
                <IconButton className="text-success" size="small" type="submit" disabled={pristine || submitting} >
                        <i className="fas fa-check" />
                </IconButton>
                <IconButton className="text-danger" size="small" onClick={onClose}>
                    <i className="fas fa-times" />
                </IconButton>
            </div>
        </form>
    );
};

export default reduxForm({
    form: "EditGender"    
})(EditGender);
