export const numberField = values => {
    const errors = {};
    const numberFields = ['maxParticipants', 'frequency'];

    numberFields.forEach(field => {
        if (values[field] && values[field] < 1) {
            errors[field] = `Invalid data`;
        }
    })
    return errors;
}