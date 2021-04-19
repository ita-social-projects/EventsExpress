export const warn = values => {
    const warnings = {}
    if (!values.type) {
        warnings.type = 'Sellect type'
    }
    if (values.type === 0) {
        warnings.type = 'Sellect point'
    } 
    return warnings
}