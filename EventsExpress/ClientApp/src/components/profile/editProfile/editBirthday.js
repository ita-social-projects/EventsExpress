import React from 'react';
import { Field, reduxForm } from 'redux-form';
import Button from '@material-ui/core/Button';
import IconButton from "@material-ui/core/IconButton";
import moment from 'moment';
import ErrorMessages from '../../shared/errorMessage';
import { renderDatePicker, parseEuDate } from '../../helpers/form-helpers';
import { fieldIsRequired } from '../../helpers/validators/required-fields-validator';
import './editFieldsStyles.css'
import EditBaseProfile from './editProfilePropertyWrapper';

// //EditProfilePropertyWrapper
// const EditBirthday = (props) => {
//     const { handleSubmit, pristine, reset, submitting, onClose } = props;

//     return (
//         <EditBaseProfile handleSubmit= {handleSubmit} pristine ={pristine} reset = {reset} submitting ={submitting} onClose ={onClose}>     
//                 <Field
//                     name="birthday"
//                     label="Birthday"
//                     minValue={MIN_DATE}
//                     maxValue={MAX_DATE}
//                     component={renderDatePicker}
//                     parse={parseEuDate}
//                 />
//          </EditBaseProfile>
//     )
// };

// export default reduxForm({
//     form: 'EditBirthday',
//     touchOnChange: true,
//     validate,
// })(EditBirthday);
