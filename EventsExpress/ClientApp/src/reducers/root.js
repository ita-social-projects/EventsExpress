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
import * as AddOccurenceEvent from './add-occurenceEvent';
import * as CancelNextOccurenceEvent from './cancel-next-occurenceEvent';
import * as CancelAllOccurenceEvents from './cancel-all-occurenceEvents';
import * as Events from './event-list';
import * as OccurenceEvents from './occurenceEvent-list';
import * as AddCategories from './add-category';
import * as Categories from './category-list';
import * as Countries from './countries';
import * as Cities from './cities';
import * as Users from './users';
import * as Roles from './roles';
import * as ChangeAvatar from './editReducers/change_avatar';
import * as ChangePassword from './editReducers/ChangePasswordReducer';
import * as EventView from './event-item-view';
import * as OccurenceEventView from './occurenceEvent-item-view';
import * as AddComment from './add-comment';
import * as DeleteComment from './delete-comment';
import * as Comments from './comment-list';
import * as RecoverPassword from './editReducers/recoverPasswordReducer'
import * as Auth from './authenticationReducer';
import * as Profile from './user-item-view';
import * as EventsForProfile from './events-for-profile';
import * as authReducer from './authReducer';
import * as UnitsOfMeasuring from './unitsOfMeasuring';

import * as Chats from './chats';
import * as Chat from './chat';
import * as ModalWind from './ModalWind';
import * as Hub from './hub';
import * as Alert from './alert';
import * as Dialog from './dialog';
import * as ContactUs from './contact-us';
import * as Notification from './notification';

const rootReducers = {
    auth: authReducer.authReducer,
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
    add_occurenceEvent: AddOccurenceEvent.reducer,
    cancel_next_occurenceEvent: CancelNextOccurenceEvent.reducer,
    cancel_all_occurenceEvent: CancelAllOccurenceEvents.reducer,
    events: Events.reducer,
    occurenceEvents: OccurenceEvents.reducer,
    unitsOfMeasuring: UnitsOfMeasuring.reducer,
    countries: Countries.reducer,
    cities: Cities.reducer,
    add_category: AddCategories.reducer,
    categories: Categories.reducer,
    users: Users.reducer,
    change_avatar: ChangeAvatar.reducer,
    changePassword: ChangePassword.reducer,
    event: EventView.reducer,
    occurenceEvent: OccurenceEventView.reducer,
    add_comment: AddComment.reducer,
    comments: Comments.reducer,
    roles: Roles.reducer,
    delete_comment: DeleteComment.reducer,
    profile: Profile.reducer,
    events_for_profile: EventsForProfile.reducer,
    recoverPassword: RecoverPassword.reducer,
    authenticate: Auth.reducer,
    chats: Chats.reducer,
    chat: Chat.reducer,
    hubConnection: Hub.reducer,
    alert: Alert.reducer,
    dialog: Dialog.reducer,
    contactUs: ContactUs.reducer,
    notification: Notification.reducer
};

export default rootReducers;
