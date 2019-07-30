import React from 'react';
import { combineReducers } from 'redux';
import { routerReducer, routerMiddleware } from 'react-router-redux';
import {reducer as LoginReducer} from './login';
import { reducer as formReducer } from 'redux-form';
import * as Username from './editReducers/editUsernameReducer';
import * as Gender from './editReducers/EditGenderReducer';
import * as Birthday from './editReducers/EditBirthdayReducer';
import * as User from './user';
import * as Register from './register';
import SelectCategories from '../components/SelectCategories/SelectCategories';
import * as AddEvent from './add-event';
import * as Events from './event-list';
import * as AddCategories from './add-category';
import * as Categories from './category-list';
import * as Countries from './countries';
import * as Cities from './cities';
import * as Users from './users';
import * as ChangeAvatar from './editReducers/change_avatar';
import * as AddComment from './add-comment';
import * as DeleteComment from './delete-comment'; 
import * as Comments from './comment-list';

const rootReducers = {
    user: User.reducer,
    routing: routerReducer,
    form: formReducer,
    login: LoginReducer,
    editUsername: Username.reducer,
    editGender: Gender.reducer,
    editBirthday: Birthday.reducer,
    form: formReducer,
    login: LoginReducer,
    register: Register.reducer,
    selectCategories: SelectCategories.reducer,
    register: Register.reducer,
    add_event: AddEvent.reducer,
    events: Events.reducer,
    countries: Countries.reducer,
    cities: Cities.reducer,
    add_category: AddCategories.reducer,
    categories: Categories.reducer,
    users: Users.reducer,
    change_avatar: ChangeAvatar.reducer,
    add_comment: AddComment.reducer,
    comments: Comments.reducer,
    delete_comment: DeleteComment.reducer
};



export default rootReducers;