

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
    SelectCategories: {
         IsSelectCategoriesSeccess: false,
         IsSelectCategoriesError: {}
     }
};

export default initialState;