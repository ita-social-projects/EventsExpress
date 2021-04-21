import { routerReducer } from 'react-router-redux';
import { reducer as LoginReducer } from './login';
import { reducer as formReducer } from 'redux-form';
import * as Username from './editReducers/editUsernameReducer';
import * as Gender from './editReducers/EditGenderReducer';
import * as Birthday from './editReducers/EditBirthdayReducer';
import * as User from './user';
import * as Register from './register';
import * as AddEvent from './add-event';
import * as AddCopyEvent from './add-copy-event';
import * as EditEventFromParent from './edit-event-from-parent';
import * as CancelNextEventSchedule from './cancel-next-eventSchedule';
import * as CancelAllEventSchedules from './cancel-all-eventSchedules';
import * as Events from './event-list';
import * as EventSchedules from './eventSchedules-list';
import * as AddCategories from './category/add-category';
import * as Categories from './category/category-list';
import * as Users from './users';
import * as Roles from './roles';
import * as ChangeAvatar from './editReducers/change_avatar';
import * as ChangePassword from './editReducers/ChangePasswordReducer';
import * as EventView from './event-item-view';
import * as EventScheduleView from './eventSchedule-item-view';
import * as AddComment from './add-comment';
import * as DeleteComment from './delete-comment';
import * as Comments from './comment-list';
import * as Tracks from './tracks/track-list';
import * as RecoverPassword from './editReducers/recoverPasswordReducer'
import * as Auth from './authenticationReducer';
import * as Profile from './user-item-view';
import * as EventsForProfile from './events-for-profile';
import * as UnitsOfMeasuring from './unitOfMeasuring/unitsOfMeasuring';
import * as AddUnitOfMeasuring from './unitOfMeasuring/add-unitOfMeasuring';
import * as Inventory from './inventory-list';
import * as UsersInventories from './usersInventories';
import * as Chats from './chats';
import * as Chat from './chat';
import * as ModalWind from './ModalWind';
import * as Hub from './hub';
import * as Alert from './alert';
import * as ContactUs from './contact-us';
import * as Notification from './notification';
import * as NotificationTypes from './notificationType/notificationType-list';
import * as Account from './account';

const rootReducers = {
    account : Account.reducer,
    modal: ModalWind.reducer,
    user: User.reducer,
    routing: routerReducer,
    form: formReducer,
    login: LoginReducer,
    editUsername: Username.reducer,
    editGender: Gender.reducer,
    editBirthday: Birthday.reducer,
    register: Register.reducer,
    add_event: AddEvent.reducer,
    add_copy_event: AddCopyEvent.reducer,
    edit_event_from_parent: EditEventFromParent.reducer,
    cancel_next_eventSchedule: CancelNextEventSchedule.reducer,
    cancel_all_eventSchedules: CancelAllEventSchedules.reducer,
    events: Events.reducer,
    eventSchedules: EventSchedules.reducer,
    inventories: Inventory.reducer,
    usersInventories: UsersInventories.reducer,
    unitsOfMeasuring: UnitsOfMeasuring.reducer,
    add_unitOfMeasuring: AddUnitOfMeasuring.reducer, add_category: AddCategories.reducer,
    categories: Categories.reducer,
    users: Users.reducer,
    change_avatar: ChangeAvatar.reducer,
    changePassword: ChangePassword.reducer,
    event: EventView.reducer,
    eventSchedule: EventScheduleView.reducer,
    add_comment: AddComment.reducer,
    comments: Comments.reducer,
    tracks: Tracks.reducer,
    roles: Roles.reducer,
    delete_comment: DeleteComment.reducer,
    profile: Profile.reducer,
    events_for_profile: EventsForProfile.reducer,
    recoverPassword: RecoverPassword.reducer,
    chats: Chats.reducer,
    chat: Chat.reducer,
    hubConnection: Hub.reducer,
    alert: Alert.reducer,
    contactUs: ContactUs.reducer,
    notification: Notification.reducer,
    notificationType: NotificationTypes.reducer,

};

export default rootReducers;
