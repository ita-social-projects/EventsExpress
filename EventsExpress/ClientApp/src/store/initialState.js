'use strict';

import filterHelper from '../components/helpers/filterHelper';

const initialState = {
    requestCount:{
        counter: 0,
    },
    requestLocalCount: {
        localCounter: 0,
    },
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
        bookmarkedEvents: []
    },
    roles: {
        data: []
    },
    login: {
        isLoginSuccess: false,
    },
    unitsOfMeasuring: {
        units: [],
        editedUnitOfMeasuring: null,
        isAdded: false,

    },
    change_avatar: {
        Update:0,
    },
    categories: {
        editedCategory: null,
        data: []
    },
    categoryGroups: {
        data: []
    },
    categoriesOfMeasuring: {
        isPending: false,
        data: []
    },
    users: {
        status: null,
        count: null,
        editedUser: null,
        userSearchFilter: null,
        data: {
            items: [],
            pageViewModel: {}
        }
    },
    comments: {
        data: {
            items: [],
            pageViewModel: {},
        }
    },
    event: {
        cancelationModalStatus: false,
        cancelation: {},
        data: [],
    },
    eventSchedule: {
        data: null
    },
    edit_event_from_parent: {
        isEventFromParentPending: false,
        isEventFromParentSuccess: false,
    },
    events: {
        data: {
            items: [],
            pageViewModel: {},
        },
        filter: filterHelper.getDefaultEventFilter(),
        layout: 'matrix',
    },
    tracks: {
        isError: false,
        data: {
            items:{},
            pageViewModel: {},
        },
    },
    inventories: {
        items: []
    },
    usersInventories: {
        data: []
    },
    eventSchedules: {
        data: {
            items: [],
        },
    },
    profile: {
        data: null
    },
    events_for_profile: {
        data: {
            items: [],
            pageViewModel: {},
        }
    },
    changePassword: {
        data: []
    },
    recoverPassword: {
    },
    authenticate: {
        data: []
    },
    hubConnections: {
      chatHub: null,
      usersHub: null
    },
    chat: {
        data: {
            messages: [],
            users: [],
            id: null
        }
    },
    chats: {
        data: []
    },
    alert: {
        variant: null,
        message: null,
        autoHideDuration: null,
        open: false
    },
    contactAdmin: {
        data: []
    },
    contactAdminList: {
        data: {
            items: [],
            pageViewModel: {},
        },
        filter: filterHelper.getDefaultContactAdminFilter(),
    },
    contactAdminItem: {
        data: []
    },
    notification:
    {
        messages: [],
        seen_messages: [],
        events: []
    },
    notificationTypes: {
        data: []
    },
    notificationTemplates: {
        data: []
    },
    notificationTemplate: {
        id: null,
        title: null,
        subject: null,
        message: null,
        availableProperties: null
    },
    config: {
        facebookClientId: null,
        googleClientId: null,
        twitterCallbackUrl: null,
        twitterConsumerKey: null,
        twitterConsumerSecret: null,
        twitterLoginEnabled:null
    }
};

export default initialState;
