
export const warn = values => {
    const warnings = {}
    if (!values.title) {
        warnings.title = 'Field is required!'
    }
    else { warnings.title = null}
    if (!values.description) {
        warnings.description = 'Field is required!'
    }
    else { warnings.description = null }
    if (!values.categories) {
        warnings.categories = 'Sellect at least 1 category'
    }
    else { warnings.categories = null }
    
    return warnings
}