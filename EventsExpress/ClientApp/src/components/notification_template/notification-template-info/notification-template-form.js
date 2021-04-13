import React, { Component } from 'react';
import { Field, reduxForm, reset } from 'redux-form';
import { connect } from 'react-redux';

class NotificationTemplateForm extends Component {
    
    render() {
        const { handleSubmit, reset } = this.props;

        return (
            <form role="form" className="d-flex flex-column mt-3" onSubmit={handleSubmit}>
                <div className="form-group">
                    <label>Title:</label>
                    <Field name="title" className="form-control" component="input" type="text" placeholder="Title..." props={{disabled: true}} />
                </div>
                <div className="form-group">
                    <label htmlFor="inpSubject">Subject:</label>
                    <Field name="subject" className="form-control" component="input" type="text" placeholder="Subject..." />
                </div>
                <div className="form-group">
                    <label htmlFor="txtMessageText">Message:</label>
                    <Field name="messageText" className="form-control" component="textarea" type="text" rows="10" placeholder="Message..." />
                </div>
                <div className="align-self-end">
                    <button type="submit" className="btn btn-success ml-0">Save</button>
                    <button type="button" className="btn btn-danger" onClick={reset}>Reset</button>
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
