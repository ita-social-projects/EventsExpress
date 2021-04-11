export const warn = values => {
    const warnings = {}
    if (values.type === 0) {
        warnings.type = 'Sellect point'
    } 
    else { warnings.type = null }
    return warnings
}