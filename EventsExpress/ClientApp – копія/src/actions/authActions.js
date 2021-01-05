export function login(token) {
    return dispath => {
        dispath({
            type: "LOGIN",
            payload: token
        });
    }
}

export function logout() {
    return dispath => {
        dispath({
            type: "LOGOUT",
            payload: ""
        });
    };
}
