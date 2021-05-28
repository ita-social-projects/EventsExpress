'use strict';

import eventHelper from '../components/helpers/eventHelper';

const initialState = {
    account:{
        linkedAuths:[],
    },
    modalWind: {
        isOpen: false
    },
    user: {
        id: null,
        name: null,
        email: null,
        phone: null,
        birthday: null,
        gender: null,
        roles: [],
        photoUrl: null,
        token: null,
        categories: [],
        notificationTypes: [],
    },
    roles: {
        isPending: false,
        data: []
    },
    login: {
        isLoginSuccess: false,
    },
    register: {
        isRegisterPending: false,
        isRegisterSuccess: false,
    },
    unitsOfMeasuring: {
        isPending: true,
        units: [],
        editedUnitOfMeasuring: null,
        isAdded: false,

    },
    add_unitOfMeasuring: {
        isUnitOfMeasuringPending: false,
        isUnitOfMeasuringSuccess: false,
    },
    change_avatar: {
        isPending: false,
        isSuccess: false,
    },
    editUsername: {
        isEditUsernamePending: false,
        isEditUsernameSuccess: false,
    },
    SelectCategories: {
        IsSelectCategoriesSeccess: false,
    },
    add_category: {
        isCategoryPending: false,
        isCategorySuccess: false,
    },
    categories: {
        isPending: false,
        editedCategory: null,
        data: []
    },
    users: {
        isPending: true,
        editedUser: null,
        userSearchFilter: null,
        data: {
            items: [],
            pageViewModel: {}
        }
    },
    add_comment: {
        isCommentPending: false,
        isCommentSuccess: false,
    },
    comments: {
        isPending: false,
        data: {
            items: [],
            pageViewModel: {},
        }
    },
    delete_comment: {
        isCommentDeletePending: false,
        isCommentDeleteSuccess: false,
    },
    event: {
        isPending: true,
        cancelationModalStatus: false,
        cancelation: {},
        data: {
            dateFrom: null,
            dateTo: null,
            photoUrl: null,
        }
    },
    eventSchedule: {
        isPending: true,
        cancelationModalStatus: false,
        cancelation: {},
        data: {
            lastRun: null,
            nextRun: null,
        }
    },
    add_event: {
        isEventPending: false,
        isEventSuccess: false,
    },
    add_copy_event: {
        isCopyEventPending: false,
        isCopyEventSuccess: false,
    },
    edit_event_from_parent: {
        isEventFromParentPending: false,
        isEventFromParentSuccess: false,
    },
    cancel_next_eventSchedule: {
        isCancelNextEventSchedulePending: false,
        isCancelNextEventScheduleSuccess: false,
    },
    cancel_eventSchedules: {
        isCancelEventSchedulesPending: false,
        isCancelEventSchedulesSuccess: false,
    },
    events: {
        isPending: true,
        data: {
            items: [],
            pageViewModel: {},
        },
        filter: eventHelper.getDefaultEventFilter(),
    },
    tracks: {
        isPending: false,
        isError: false,
        data: {
            items:{},
            pageViewModel: {},
        },
    },
    inventories: {
        isPending: true,
        items: []
    },
    usersInventories: {
        isPending: true,
        data: []
    },
    eventSchedules: {
        isPending: true,
        data: {
            items: [],
        },
    },
    profile: {
        isPending: true,
        data: null
    },
    events_for_profile: {
        isPending: true,
        data: {
            items: [],
            pageViewModel: {},
        }
    },
    changePassword: {
        isPending: false,
        data: []
    },
    recoverPassword: {
        isPending: false,
        isSucces: null,
    },
    authenticate: {
        isPending: false,
        isSucces: false,
        data: []
    },
    hubConnection: null,
    chat: {
        isPending: false,
        isSuccess: false,
        data: {
            messages: [],
            users: [],
            id: null
        }
    },
    chats: {
        isPending: false,
        isSuccess: false,
        data: []
    },
    alert: {
        variant: null,
        message: null,
        autoHideDuration: null,
        open: false
    },
    contactUs: {
        isPending: false,
        isSuccess: false,
    },
    notification:
    {
        messages: [],
        seen_messages: [],
        events: []
    },
    notificationTypes: {
        isPending: false,
        data: []
    },
    notificationTemplates: {
        data: []
    },
    notificationTemplate: {
        id: null,
        title: null,
        subject: null,
        message: null  
    },
};

export default initialState;
