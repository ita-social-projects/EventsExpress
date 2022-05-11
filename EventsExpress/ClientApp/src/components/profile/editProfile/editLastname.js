import React from 'react';
import { Field, reduxForm } from 'redux-form';
import IconButton from "@material-ui/core/IconButton";
import ErrorMessages from '../../shared/errorMessage';
import { renderTextField } from '../../helpers/form-helpers';
import './editFieldsStyles.css';

const EditLastname = props => {
    const { handleSubmit, pristine, reset, submitting, onClose } = props;
    return (
        <form onSubmit={handleSubmit} className='field'>
            <div className='content'>
                <Field
                    name="lastName"
                    component={renderTextField}
                    label="LastName"
                />
                {
                    props.error &&
                    <ErrorMessages error={props.error} className="text-center" />
                }
            </div>
            <div className='editFieldButtons'>
                <IconButton className="text-success" size="small" type="submit" disabled={pristine || submitting} >
                        <i className="fas fa-check" />
                </IconButton>
                <IconButton className="text-danger" size="small" onClick={reset && onClose}>
                    <i className="fas fa-times" />
                </IconButton>
            </div>
        </form>
    );
};

export default reduxForm({
    form: 'EditLastname',
    
})(EditLastname);