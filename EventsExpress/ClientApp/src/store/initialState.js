

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
        loginError: null
    },
    register:{
        isRegisterPending: false,
        isRegisterSuccess: false,
        registerError: null
    },
    
    editUsername: {
        isEditUsernamePending: false,
        isEditUsernameSuccess: false,
        EditUsernameError: {}
    }
     },
    SelectCategories: {
         IsSelectCategoriesSeccess: false,
         IsSelectCategoriesError: null
     }
};

export default initialState;