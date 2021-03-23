import React, { Component } from 'react';
import { Field, reduxForm } from 'redux-form';
import Button from "@material-ui/core/Button";
import CheckboxGroup from './CheckboxGroup';
import momentLocaliser from 'react-widgets-moment';
import ErrorMessages from '../shared/errorMessage';
import moment from 'moment';
momentLocaliser(moment);

class SelectNotificationType extends Component {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        this.props.initialize({
            notificationTypes: this.props.initialValues.notificationTypes
        });
    }
   

    render() {
        const { handleSubmit, submitting, items, error } = this.props;
        return (

            <div >
                <form onSubmit={handleSubmit}>
                    <Field name="notificationTypes"
                        component={CheckboxGroup}
                        options={items}                        
                    />
                    {
                         error &&
                        <ErrorMessages error={error} className="text-center" />
                    }
                    <div>
                        <Button
                            type="submit"
                            color="primary"
                            disabled={submitting} >
                            Save
                        </Button >
                    </div>
                </form>
            </div>
        );
    }
}

export default reduxForm({
    form: 'SelectNotificationType',
    enableReinitialize: true
})(SelectNotificationType)