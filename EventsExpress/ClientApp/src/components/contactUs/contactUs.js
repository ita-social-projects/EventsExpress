import React, { Component } from 'react';
import { reduxForm, Field } from 'redux-form';
import Button from '@material-ui/core/Button';
import { renderTextArea, renderTextField } from '../helpers/helpers';
import Module from '../helpers';
import ErrorMessages from '../shared/errorMessage';

const { validate } = Module;

class ContactUs extends Component {

    render() {
        const { pristine, reset, submitting, error } = this.props;
        return (
            <div id='notfound'>
                <div className='notfound'>
                    <h1 className='f1'>{'Contact Us'}</h1>
                    <form className="notfound-404" onSubmit={this.props.handleSubmit}>
                        <div className="box text text-2 pl-md-4 " >
                            <Field
                                name="email"
                                className="form-control"
                                component={renderTextField}
                                label="Your e-mail:"
                            />

                            <p></p><p></p><p></p>
                            <div className="text-left mb-2">Problem Type</div>
                            <Field
                                name='type'
                                className="form-control"
                                component="select">
                                <option value="newCategory">New Category</option>;
                                <option value="bugReport">Bug Report</option>;
                                <option value="badEvent">Bad Event</option>;
                                <option value="bugUser">Bad User</option>;
                             </Field>

                            <p></p><p></p><p></p>
                            <Field
                                name='description'
                                className="form-control"
                                component={renderTextArea}
                                type="input"
                                label="Description" />
                        </div>
                        {error && <ErrorMessages error={error} className="text-center" />}
                        <Button
                            type="submit"
                            color="primary"
                            disabled={pristine || submitting}>
                            Submit
                             </Button>
                        <Button
                            type="button"
                            color="primary"
                            disabled={pristine || submitting}
                            onClick={reset}>
                            Clear
                             </Button>
                    </form>
                </div>
            </div>
        )
    }
}

export default reduxForm({
    form: "ContactUs",
    validate
})(ContactUs);