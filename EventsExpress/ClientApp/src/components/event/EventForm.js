import React, { Component } from 'react';
import { reduxForm, Field } from 'redux-form';

export class EventForm extends Component {
    constructor(props) {
        super(props);
    }

    render() {

        return (
            <form onSubmit={ this.onSubmit } enctype="multipart/form-data">

                <button type="submit" name="submit" class="btn btn-default">Save</button>

            </form>
        );
    }

}

EventForm = reduxForm({
    form: 'event-form'
})(EventForm);

export default EventForm;
