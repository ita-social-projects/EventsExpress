import React, { Component } from 'react';
import { reduxForm, Field } from 'redux-form';

export class EventForm extends Component {
    constructor(props) {
        super(props);
    }

    render() {

        return (
            <form onSubmit={ this.onSubmit } enctype="multipart/form-data">
                    <div class="form-group">
                        <label htmlFor="Title" class="control-label">Title</label>
                    <Field name="Title" component="input" required class="form-control" />
                    </div>
                    <div class="form-group">
                    <label htmlFor="DateFrom" class="control-label">Date from</label>
                    <Field name="DateFrom" type="date" component="input" required class="form-control" />
                    </div>
                    <div class="form-group">
                    <label htmlFor="DateTo" class="control-label">Date to</label>
                    <Field name="DateTo" type="date" component="input" class="form-control" />
                    </div>
                   

                    <div class="form-group">
                    <label htmlFor="Description" class="control-label">Description</label>
                    <Field name="Description" component="input" class="form-control" />
                    </div>

                <button type="submit" name="submit" class="btn btn-default">Save</button>
                

                </form>
        );
    }

}

EventForm = reduxForm({
    form: 'event-form'
})(EventForm);

export default EventForm;
