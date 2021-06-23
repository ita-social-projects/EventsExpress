const minLength = min => value =>
    value && value.length < min
        ? `Must be ${min} characters or more`
        : undefined;
const maxLength = max => value =>
    value && value.length > max
        ? `Must be ${max} characters or less`
        : undefined

export const minLength10 = minLength(10);
export const minLength20 = minLength(20);
export const maxLength15 = maxLength(15);
export const maxLength30 = maxLength(30);
export const minLength6 = minLength(6);