import React from "react";
import { Field, reduxForm } from "redux-form";
import { renderSelectField } from '../helpers/form-helpers'
import ErrorMessages from '../shared/errorMessage';

let editGenderRegister = props => {
    const { handleSubmit, pristine, submitting } = props;
    return (
        render(
        <form name= "editGenderRegister" onSubmit={handleSubmit}>
            <div>
                <Field
                    minWidth={140}
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
            
        </form>
        )
    );
};

export default reduxForm({
    form: "editGenderRegister"    
})(editGenderRegister);
