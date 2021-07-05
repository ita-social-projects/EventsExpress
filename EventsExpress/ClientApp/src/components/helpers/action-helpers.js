import 'react-widgets/dist/css/react-widgets.css';
import "react-datepicker/dist/react-datepicker.css";

export const buildValidationState = async (responseData) => {
    let result = {};
    let jsonRes = await responseData.json();

    for (const [key, value] of Object.entries(jsonRes.errors)) {
        if (key == "") {
            result = { ...result, _error: value };
        }
        else {
            result = { ...result, [key]: value };
        }
    }
    return result;
}

export const getErrorMessage = async (responseData) => {
    let jsonRes = await responseData.json();

    for (const [key, value] of Object.entries(jsonRes[""].errors)) {
        if (key === "0") {
            return `Error : ${value["errorMessage"]}`;
        }
        return `Error for ${key}: ${value["errorMessage"]}`;
    }
    return "Something went wrong.";
}
