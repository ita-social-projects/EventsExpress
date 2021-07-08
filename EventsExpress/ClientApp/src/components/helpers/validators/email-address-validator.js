export const isValidEmail = email => {
    const errors = {};
    if (email &&
        !/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(email)) {
        errors.email = 'Invalid email address'
    }
    return errors;
}

