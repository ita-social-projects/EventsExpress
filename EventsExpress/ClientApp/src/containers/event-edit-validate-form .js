import { numberField } from '../components/helpers/validators/number-fields-validator';
import { requiredField } from '../components/helpers/validators/required-fields-validator';

export const validate = values => {
    let errors = {};
    const occurenceFields = ['periodicity', 'frequency'];
    occurenceFields.forEach(field => {
        if ('occurenceFields'.checked && !values[field]) {
            errors[field] = 'Required'
        }
    })

    var requiredErrors = requiredField(values);
    var numberErrors = numberField(values);
    errors = { ...errors, ...numberErrors, ...requiredErrors };

    if (values.categories != null && values.categories.length == 0) {
        errors.categories = "Required";
    }
    if (!values.selectedPos || values.selectedPos == "") {
        errors.selectedPos = "Required";
    }
    if (values.maxParticipants && values.maxParticipants < 1) {
        errors.maxParticipants = `Invalid data`;
    }
    if (values.image != null && values.image.file != null && values.image.file.size < 4096)
        errors.image = "Image is too small";
    return errors;
}