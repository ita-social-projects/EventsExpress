

const initialState = {
    user:{
        id: null,
        name: null,
        email: null,
        phone: null,
        birthday: null,
        gender: null,
        role: null,
        photoUrl: null,
        token: null
    },
    login:{
        isLoginPending: false,
        isLoginSuccess: false,
        loginError: {}
    },
    
    editUsername: {
        isEditUsernamePending: false,
        isEditUsernameSuccess: false,
        EditUsernameError: {}
    }
};

export default initialState;