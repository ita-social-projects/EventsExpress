export const warn = values => {
    const warnings = {}
    if (!values.maxParticipants) {
        warnings.maxParticipants = 'Field is required!'
    }
    if (values.maxParticipants <= 0) {
        warnings.maxParticipants = 'Number must be grater than 0!'
    }
    return warnings
}