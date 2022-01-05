import { combineReducers } from 'redux';
import { locationFilterReducer } from './location-filter';
import { usersDataReducer } from './users-data';

export const reducer = combineReducers({
    users: usersDataReducer,
    locationFilter: locationFilterReducer,
});
