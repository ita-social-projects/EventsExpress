
const initialState = {
    modalWind:{
        isOpen: false 
    },
    resetError: {
        isError: false,
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
        editedCategory: null,
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
        editedUser: null,
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
            
            dateFrom: null,
            dateTo: null,
            photoUrl: null
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
    },
    changePassword: {
        isPending: false,
        isError: false,
        data: []
    },
    recoverPassword: {
        isPending: false,
        isError: false,
        isSucces:null,
    },
    authenticate:{
        isPending:false,
        isSucces:false,
        isError:null,
        data:[]
    },
    chats:{
        isPending: false,
        isSuccess:false,
        isError:null,
        data: []
    },
    hubConnection: null,
    chat:{
        isPending: false,
        isSuccess:false,
        isError:null,
        data: {
            messages: [],
            users: []
        }
    },
    alert:{
        variant:null,
        className:null,
        message:null,
        open:false
    }
};

export default initialState;