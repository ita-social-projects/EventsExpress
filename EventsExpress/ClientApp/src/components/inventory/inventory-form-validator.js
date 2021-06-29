export const validate = values => {
    const errors = {};

    const requiredFields = [
        'itemName',
        'needQuantity',
        'unitOfMeasuring.id'
    ];
    requiredFields.forEach(field => {
        if (!values[field]) {
            errors[field] = 'Required'
        }
    });
    if (values.itemName && values.itemName.length > 30) {
        errors.itemName = `Invalid length: 1 - 30 symbols`;
    }
    if (values.needQuantity && values.needQuantity <= 0) {
        errors.needQuantity = `Can not be 0 or negative`;
    }
    if (!values.unitOfMeasuring.id) {
        errors.unitOfMeasuring = "Required";
    }
    return errors;
}
