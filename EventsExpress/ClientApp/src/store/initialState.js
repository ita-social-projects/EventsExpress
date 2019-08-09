
const initialState = {
    modalWind:{
        isOpen: false 
    },
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
    roles: {
        isPending: false,
        isError: false,
        data: []
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
        isPending: true,
        isError: false,
        data: {
            items: [],
            pageViewModel: {}

        }
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
        data: {
            items: [],
            pageViewModel: {}

        }
    },
    add_comment: {
        isCommentPending: false,
        isCommentSuccess: false,
        commentError: null
    },
    comments: {
        isPending: false,
        isError: false,
        data: {
            items: [],
            pageViewModel: {}

        }
    },
    delete_comment: {
        isCommentDeletePending: false,
        isCommentDeleteSuccess: false,
        commentDeleteError: null
    },
    event: {
        isPending: true,
        isError: false,
        data: {
            dateFrom: null
        } 
    },
    profile: {
        isPending: true,
        isError: false,
        data: null
    },
    events_for_profile: {
        isPending: true,
        isError: false,
        data: null
    }
};

export default initialState;