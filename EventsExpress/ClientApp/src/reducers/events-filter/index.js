import { combineReducers } from 'redux';
import { usersDataReducer } from './users-data';
import { locationFilterReducer } from './location-filter';
export const reducer = combineReducers({
    users: usersDataReducer,
    locationFilter: locationFilterReducer,
});
