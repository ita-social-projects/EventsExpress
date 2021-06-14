
export const REQUEST_INC = "REQUEST_INC";
export const REQUEST_DEC = "REQUEST_DEC";

//function means that request is in progress
export function getRequestInc() {
    return {
        type: REQUEST_INC,
    };
}

//function means that request finished
export function getRequestDec() {
    return {
        type: REQUEST_DEC,
    };
}