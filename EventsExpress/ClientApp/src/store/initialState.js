
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
        token: null,
        categories: []
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
    add_event:{
        isEventPending: false,
        isEventSuccess: false,
        eventError: null
    },
    events: {
        isPending: false,
        isError: false,
        data: []
    },
    change_avatar: {
        isPending: false,
        isSuccess: false,
        Error: {}
     },
    editUsername: {
        isEditUsernamePending: false,
        isEditUsernameSuccess: false,
        EditUsernameError: {}
     },
    SelectCategories: {
         IsSelectCategoriesSeccess: false,
         IsSelectCategoriesError: null
     },
    add_event:{
        isEventPending: false,
        isEventSuccess: false,
        eventError: null
    },
    events: {
        isPending: false,
        isError: false,
        data: []
    },
    add_category: {
        isCategoryPending: false,
        isCategorySuccess: false,
        categoryError: null
    },
    categories: {
        isPending: false,
        isError: false,
        data: []
    },
    countries: {
        isPending: false,
        isError: false,
        data: []
    },
    cities: {
        isPending: false,
        isError: false,
        data: []
    },
    users: {
        isPending: false,
        isError: false,
        data: []
    },
    changePassword: {
        isPending: false,
        isError: false,
        data: []
    }

};

export default initialState;