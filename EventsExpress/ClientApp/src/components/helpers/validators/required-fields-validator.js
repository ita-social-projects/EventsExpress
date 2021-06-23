export const requiredField = values => {
    const errors = {};
    const requiredFields = [
        'title',
        'description',
        'categories',
        'image'
    ];

    requiredFields.forEach(field => {
        if (!values[field]) {
            errors[field] = 'Required'
        }
    });
    return errors;
}