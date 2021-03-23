'use strict';

import eventHelper from '../components/helpers/eventHelper';

const initialState = {
    modalWind: {
        isOpen: false
    },
    resetError: {
        isError: false,
    },
    user: {
        id: null,
        name: null,
        email: null,
        phone: null,
        birthday: null,
        gender: null,
        role: null,
        photoUrl: null,
        token: null,
        categories: [],
        notificationTypes: [],
    },
    roles: {
        isPending: false,
        isError: false,
        data: []
    },
    login: {
        isLoginSuccess: false,
    },
    register: {
        isRegisterPending: false,
        isRegisterSuccess: false,
        registerError: null
    },
    unitsOfMeasuring: {
        isPending: true,
        isError: false,
        units: [],
        editedUnitOfMeasuring: null,
        isAdded: false,

    },
    add_unitOfMeasuring: {
        isUnitOfMeasuringPending: false,
        isUnitOfMeasuringSuccess: false,
        unitOfMeasuringError: null
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
    users: {
        isPending: true,
        isError: false,
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
        data: null,
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
        isError: false,
        data: {
            items: [],
            pageViewModel: {},
        },
        filter: eventHelper.getDefaultEventFilter(),
    },
    inventories: {
        isPending: true,
        listInventoriesErrorMessage: false,
        setItemErrorMessage: null,
        items: []
    },
    usersInventories: {
        isPending: true,
        isError: false,
        data: []
    },
    eventSchedules: {
        isPending: true,
        isError: false,
        data: {
            items: [],
        },
    },
    profile: {
        isPending: true,
        isError: false,
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
        isError: false,
        data: []
    },
    recoverPassword: {
        isPending: false,
        isError: false,
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
        isError: null,
        data: {
            messages: [],
            users: [],
            id: null
        }
    },
    chats: {
        isPending: false,
        isSuccess: false,
        isError: null,
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
        isError: null
    },
    notification:
    {
        messages: [],
        seen_messages: [],
        events: []
    },
    notificationTypes: {
        isPending: false,
        isError: false,
        data: []
    }
};

export default initialState;
