import React from 'react';
import { combineReducers } from 'redux';
import { routerReducer, routerMiddleware } from 'react-router-redux';
import {reducer as LoginReducer} from './login';
import { reducer as formReducer } from 'redux-form';
import * as Edit from './editReducers/editUsernameReducer';
import * as User from './user';

const rootReducers = {
    user: User.reducer,
    routing: routerReducer,
    form: formReducer,
    login: LoginReducer,
    editUsername: Edit.reducer
};



export default rootReducers;