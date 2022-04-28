import React, {Component} from "react";
import {Field, reduxForm} from "redux-form";
import {minLength10, minLength20} from "../../components/helpers/validators/min-max-length-validators";
import {Button} from "@material-ui/core";

class CategoryGroupInfoForm extends Component {

    render() {
        const { handleSubmit, submitting, reset, pristine, availableProps } = this.props;
        const { renderField, renderProperties } = this;

        return (
            <div className="d-flex">
                <form role="form" className="d-flex flex-grow-1 flex-column mt-3 ml-0 w-100 float-left"
                      onSubmit={handleSubmit}>
                    <Field
                        name="title"
                        type="text"
                        component={renderField}
                        label="Title"
                        InputProps={{
                            readOnly: true,
                        }}
                    />
                    <Field
                        name="subject"
                        className="form-control"
                        type="text"
                        component={renderField}
                        label="Subject"
                        inputProps={{
                            required: true,
                        }}
                        validate={[minLength10]}
                    />
                    <Field
                        name="message"
                        className="form-control"
                        component={renderField}
                        type="text"
                        rows={15}
                        label="Message"
                        multiline
                        inputProps={{
                            required: true,
                        }}
                        variant="outlined"
                        validate={[minLength20]}
                    />
                    <div className="align-self-end">
                        <Button type="submit" disabled={submitting} color="primary">Save</Button>
                        <Button type="button" color="secondary" disabled={pristine || submitting} onClick={reset}>
                            Reset
                        </Button>
                    </div>
                </form>
                <div className="ml-4 mt-6">
                    {availableProps && renderProperties(availableProps)}
                </div>
            </div>
        )
    }
}

CategoryGroupInfoForm = reduxForm({
    form: 'CategoryGroupInfoForm',
    enableReinitialize: true
})(CategoryGroupInfoForm);

export default CategoryGroupInfoForm;