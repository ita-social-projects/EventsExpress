import moment from 'moment';
import { fieldIsRequired } from '../../components/helpers/validators/required-fields-validator';
const today = () => moment().startOf('day');

const MIN_AGE = 14;
const MAX_AGE = 115;
const MIN_DATE = today().subtract(MAX_AGE, 'years');
const MAX_DATE = today().subtract(MIN_AGE, 'years');

export const validate = values => {
    const errors = {};
    const requiredFields = ['birthday'];

    if (values.birthday === undefined) {
        return fieldIsRequired(values, requiredFields);
    }

    const dateOfBirth = moment(values.birthday);
    if (values.birthday === null || dateOfBirth > today()) {
        errors.birthday = 'It is possible to use only valid date of birth';
    } else if (dateOfBirth > MAX_DATE) {
        errors.birthday = 'You must be 14 years old or over to use the website';
    } else if (dateOfBirth <= MIN_DATE) {
        errors.birthday = 'You must be less than 115 years old to use the website';
    }
    
    return errors;
};