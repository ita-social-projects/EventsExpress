import React from "react";
import { Field, reduxForm } from "redux-form";
import Module from '../../helpers';
import Button from "@material-ui/core/Button";
import ErrorMessages from '../../shared/errorMessage';

const { validate, renderDatePicker } = Module;
const EditBirthday = props => {
    const minValue = new Date().getFullYear() - 115;
    const maxValue = new Date().getFullYear() - 15;
    const { handleSubmit, pristine, reset, submitting } = props;
    return (
        <form onSubmit={handleSubmit}>
            <div>
                <Field
                    name="Birthday"
                    id="date"
                    label="Birthday"
                    minValue={minValue}
                    maxValue={maxValue}
                    component={renderDatePicker}
                    InputLabelProps={{
                        shrink: true
                    }}
                />
                {
                    props.error &&
                    <ErrorMessages error={props.error} className="text-center" />
                }
            </div>
            <div>
                <Button type="submit" color="primary" disabled={pristine || submitting}>
                    Submit
        </Button>
                <Button type="button" color="primary" disabled={pristine || submitting} onClick={reset}>
                    Clear
        </Button>
            </div>
        </form>
    );
};

export default reduxForm({
    form: "EditBirthday",
    validate
})(EditBirthday);
