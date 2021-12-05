import 'react-widgets/dist/css/react-widgets.css';
import 'react-datepicker/dist/react-datepicker.css';

export const buildValidationState = async responseData => (await responseData.json()).errors;

export const getErrorMessage = async responseData => {
    const entries = Object.entries(await buildValidationState(responseData));
    if (entries.length === 0) {
        return 'Something went wrong.';
    }

    const [key, value] = entries[0];
    if (key === '_error') {
        return `Error : ${value[0]}`;
    }

    return `Error for ${key}: ${value[0]}`;
};
