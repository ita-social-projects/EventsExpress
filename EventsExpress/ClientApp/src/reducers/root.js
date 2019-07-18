import React from 'react';
import { combineReducers } from 'redux';
import { routerReducer, routerMiddleware } from 'react-router-redux';
import {reducer as LoginReducer} from './login';
import { reducer as formReducer } from 'redux-form';
import * as User from './user';
import * as Register from './register';
import SelectCategories from '../components/SelectCategories/SelectCategories';

const rootReducers = {
    user: User.reducer,
    routing: routerReducer,
    form: formReducer,
    login: LoginReducer,
    register: Register.reducer,
    selectCategories: SelectCategories.reducer
};



export default rootReducers;