import { fieldIsRequired } from '../helpers/validators/required-fields-validator';
import { maxLength30 } from '../helpers/validators/min-max-length-validators';

export const validate = values => {
    const errors = {};

    const requiredFields = [
        'itemName',
        'needQuantity',
        'unitOfMeasuring.id'
    ];

    if (maxLength30(values.itemName)) {
        errors.itemName = `Invalid length: 1 - 30 symbols`;
    }
    if (values.needQuantity && values.needQuantity <= 0) {
        errors.needQuantity = `Can not be 0 or negative`;
    }
    if (!values.unitOfMeasuring.id) {
        errors.unitOfMeasuring = "Required";
    }
    return {
        ...fieldIsRequired(values, requiredFields),
        ...errors
    }
}
