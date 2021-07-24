import { numberField } from '../components/helpers/validators/number-fields-validator';
import { fieldIsRequired } from '../components/helpers/validators/required-fields-validator';

export const validate = values => {
    let errors = {};
    const occurenceFields = ['periodicity', 'frequency'];
    const requiredFields = [
        'title',
        'description',
        'categories'
    ];
    occurenceFields.forEach(field => {
        if ('occurenceFields'.checked && !values[field]) {
            errors[field] = 'Required'
        }
    })

    if (values.categories != null && values.categories.length == 0) {
        errors.categories = "Required";
    }
    if (!values.selectedPos || values.selectedPos == "") {
        errors.selectedPos = "Required";
    }
    if (values.maxParticipants && values.maxParticipants < 1) {
        errors.maxParticipants = `Invalid data`;
    }

    if (values.location !== null && values.location.type === 1 && values.location.onlineMeeting === null) {
        errors.location = {};
        errors.location.onlineMeeting = "URL cannot be empty";
    }


    return {
        ...errors,
        ...numberField(values),
        ...fieldIsRequired(values, requiredFields),
    }
}