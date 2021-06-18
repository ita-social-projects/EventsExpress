export const validate = values => {
    const errors = {};
    const numberFields = ['maxParticipants', 'frequency'];
    const occurenceFields = ['periodicity', 'frequency'];
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

    numberFields.forEach(field => {
        if (values[field] && values[field] < 1) {
            errors[field] = `Invalid data`;
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
    if (values.image != null && values.image.file != null && values.image.file.size < 4096)
        errors.image = "Image is too small";

    occurenceFields.forEach(field => {
        if ('checkOccurence'.checked && !values[field]) {
            errors[field] = 'Required'
        }
    })
    return errors;
}