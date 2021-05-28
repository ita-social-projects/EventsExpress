import React, { Component } from 'react';
import { reduxForm, Field, getFormValues } from 'redux-form';
import { connect } from 'react-redux';
import Button from '@material-ui/core/Button';
import { renderTextArea, renderTextField } from '../helpers/helpers';
import Module from '../helpers';
import ErrorMessages from '../shared/errorMessage';
import issueTypeEnum from '../../constants/issueTypeEnum ';


const { validate } = Module;

class ContactAdmin extends Component {


    render() {
        const { pristine, reset, submitting, error } = this.props;
        return (
            <div id='notfound'>
                <div className='notfound'>
                    <h1 className='f1'>{'Contact Us'}</h1>
                    <form className="notfound-404" onSubmit={this.props.handleSubmit}>
                        <div className="box text text-2 pl-md-4 " >
                            {(this.props.user.role === "User")
                                ? <Field
                                    name="email"
                                    className="form-control"
                                    component={renderTextField}
                                    value={this.props.email}
                                    label="Your e-mail:"
                                />
                                : <Field
                                    name="email"
                                    className="form-control"
                                    component={renderTextField}
                                    label="Your e-mail:"
                                />
                            }
                            <p></p><p></p><p></p>
                            <div className="text-left mb-2">Problem Type</div>
                            <Field
                                name='subject'
                                className="form-control"
                                component="select"
                                parse={value => Number(value)}
                            >
                                <option value={issueTypeEnum.NewCategory}>New Category</option>;
                                <option value={issueTypeEnum.BugReport}>Bug Report</option>;
                                <option value={issueTypeEnum.BadEvent}>Bad Event</option>;
                                <option value={issueTypeEnum.BadUser}>Bad User</option>;
                                <option value={issueTypeEnum.Other}>Other</option>;
                             </Field>


                            {(this.props.form_values !== undefined
                                && this.props.form_values.subject == issueTypeEnum.Other
                                && <Field
                                    name="title"
                                    className="form-control"
                                    component={renderTextField}
                                    label="Enter problem type:"
                                />
                            )}

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

const mapStateToProps = (state) => {
    return {
        initialValues: { email: state.user.email },
        form_values: getFormValues('ContactAdmin')(state),
    }
}



export default connect(mapStateToProps)(reduxForm({
    form: "ContactAdmin",
    validate,
    enableReinitialize: true
})(ContactAdmin));