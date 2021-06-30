export const fieldIsRequired = ( values, requiredFields) => {
    const errors = {};
    requiredFields.forEach(field => {
        if (!values[field]) {
            errors[field] = 'Required'
        }
    });
    return errors;
}
