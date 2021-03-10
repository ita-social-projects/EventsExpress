import React from "react";
import { Field, reduxForm } from "redux-form";
import { renderTextField } from '../helpers/helpers';

const BlockEventForm = (props) => {
    const { handleSubmit, pristine, submitting, reset } = props
    return (
        <form onSubmit={handleSubmit}>
            <div>
                <Field
                    className="form-control"
                    name='changeStatusReason'
                    component={renderTextField}
                    type="text"
                    size="50"
                    label="Enter the reason"
                />
            </div>
            <div>
                <button type="button" disabled={pristine || submitting} onClick={reset}>Discard</button>
                <button type="submit" disabled={pristine || submitting} >Confirm action</button>
            </div>
        </form>
    )
}

export default reduxForm({
    form: "block-form",
})(BlockEventForm);
