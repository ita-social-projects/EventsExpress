import React, { Component } from 'react';
import { Field, reduxForm, reset } from 'redux-form';
import { connect } from 'react-redux';
import { TextField, Button } from '@material-ui/core'
import { minLength10, minLength20 } from '../../helpers/validators';

class NotificationTemplateForm extends Component {

    renderField = ({ input, meta: { error }, ...props }) => (
        <div className="form-group">
            <TextField
                {...input}
                {...props}
                error={Boolean(error)}
                helperText={error}
            />
        </div>
    );

    render() {
        const { handleSubmit, submitting, reset, pristine } = this.props;

        return (
            <form role="form" className="d-flex flex-column mt-3 ml-0" onSubmit={handleSubmit}>
                <Field
                    name="title"
                    type="text"
                    component={this.renderField}
                    label="Title"
                    InputProps={{
                        readOnly: true,
                    }}
                />
                <Field
                    name="subject"
                    className="form-control"
                    type="text"
                    component={this.renderField}
                    label="Subject"
                    inputProps={{
                        required: true,
                    }}
                    validate={[minLength10]}
                />
                <Field
                    name="message"
                    className="form-control"
                    component={this.renderField}
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
                    <Button type="button" color="secondary" disabled={pristine || submitting} onClick={reset}>Reset</Button>
                </div>
            </form>
        )
    }
}

NotificationTemplateForm = reduxForm({
    form: 'notificationTemplateForm',
    enableReinitialize: true
})(NotificationTemplateForm);

const mapDispatchToProps = (dispatch) => ({
    reset: () => dispatch(reset('notificationTemplateForm'))
});

export default connect(mapDispatchToProps)(NotificationTemplateForm);
