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
import * as AddEvent from './add-event';
import * as Events from './event-list';
import SelectCategories from '../components/SelectCategories/SelectCategories';

const rootReducers = {
    user: User.reducer,
    routing: routerReducer,
    form: formReducer,
    login: LoginReducer,
    editUsername: Username.reducer,
    editGender: Gender.reducer,
    editBirthday: Birthday.reducer,
    form: formReducer,

    selectCategories: SelectCategories.reducer,
    register: Register.reducer,
    add_event: AddEvent.reducer,
    events: Events.reducer
};



export default rootReducers;