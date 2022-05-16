import React from 'react';
import './editFieldsStyles.css';
import { IconButton } from '@material-ui/core';
import { reduxForm } from 'redux-form';
import {validate} from '../../../containers/editProfileContainers/validateBirthday'
const EditProfilePropertyWrapper = (props) => {
    const { handleSubmit, pristine, reset, submitting, onClose } = props;

    return (
        <form onSubmit ={handleSubmit} >
            <div className="content">
                {
                    props.children
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
    )
}

export default reduxForm({
    form: 'EditProfile',
    validate
})(EditProfilePropertyWrapper)