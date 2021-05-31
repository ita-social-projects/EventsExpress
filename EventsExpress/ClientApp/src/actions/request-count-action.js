
export const REQUEST_INC = "REQUEST_INC";
export const REQUEST_DEC = "REQUEST_DEC";

export function getRequestInc() {
    return {
        type: REQUEST_INC,
    };
}

export function getRequestDec() {
    return {
        type: REQUEST_DEC,
    };
}