import { routerReducer } from 'react-router-redux';
import { reducer as LoginReducer } from './login';
import { reducer as formReducer } from 'redux-form';
import * as User from './user';
import * as EditEventFromParent from './edit-event-from-parent';
import * as Events from './event-list';
import * as EventSchedules from './eventSchedules-list';
import * as Categories from './category/category-list';
import * as Users from './users';
import * as Roles from './roles';
import * as ChangeAvatar from './editReducers/change_avatar';
import * as EventView from './event-item-view';
import * as EventScheduleView from './eventSchedule-item-view';
import * as Comments from './comment-list';
import * as Tracks from './tracks/track-list';
import * as RecoverPassword from './editReducers/recoverPasswordReducer';
import * as Profile from './user-item-view';
import * as EventsForProfile from './events-for-profile';
import * as UnitsOfMeasuring from './unitOfMeasuring/unitsOfMeasuring';
import * as Inventory from './inventory-list';
import * as UsersInventories from './usersInventories';
import * as Chats from './chats';
import * as Chat from './chat';
import * as ModalWind from './ModalWind';
import * as Hub from './hub';
import * as Alert from './alert';
import * as ContactAdmin from './contactAdmin/contact-admin-reducer';
import * as ContactAdminList from './contactAdmin/contact-admin-list-reducer';
import * as ContactAdminIssueStatus from './contactAdmin/contact-admin-issue-status-reducer';
import * as ContactAdminItem from './contactAdmin/contact-admin-item-reducer';
import * as Notification from './notification';
import * as NotificationTypes from './notificationType/notificationType-list';
import * as Account from './account';
import * as NotificationTemplates from "./notification-templates/notification-templates";
import * as NotificationTemplate from "./notification-templates/notification-template";
import * as Config from './config';
import * as RequestCount from "./request-index-count"

const rootReducers = {
    requestCount: RequestCount.reducer,
    account : Account.reducer,
    modal: ModalWind.reducer,
    user: User.reducer,
    routing: routerReducer,
    form: formReducer,
    login: LoginReducer,
    edit_event_from_parent: EditEventFromParent.reducer,
    events: Events.reducer,
    eventSchedules: EventSchedules.reducer,
    inventories: Inventory.reducer,
    usersInventories: UsersInventories.reducer,
    unitsOfMeasuring: UnitsOfMeasuring.reducer,
    categories: Categories.reducer,
    users: Users.reducer,
    change_avatar: ChangeAvatar.reducer,
    event: EventView.reducer,
    eventSchedule: EventScheduleView.reducer,
    comments: Comments.reducer,
    tracks: Tracks.reducer,
    roles: Roles.reducer,
    profile: Profile.reducer,
    events_for_profile: EventsForProfile.reducer,
    recoverPassword: RecoverPassword.reducer,
    chats: Chats.reducer,
    chat: Chat.reducer,
    hubConnection: Hub.reducer,
    alert: Alert.reducer,
    contactAdmin: ContactAdmin.reducer,
    contactAdminList: ContactAdminList.reducer,
    contactAdminIssueStatus: ContactAdminIssueStatus.reducer,
    contactAdminItem: ContactAdminItem.reducer,
    notification: Notification.reducer,
    notificationType: NotificationTypes.reducer,
    NotificationTemplates: NotificationTemplates.reducer,
    NotificationTemplate: NotificationTemplate.reducer,
    config: Config.reducer
};

export default rootReducers;
