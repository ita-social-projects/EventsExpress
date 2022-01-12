import { combineReducers } from 'redux';
import { usersDataReducer } from './users-data';

export const reducer = combineReducers({
    users: usersDataReducer
});
