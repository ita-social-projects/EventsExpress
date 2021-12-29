import { combineReducers } from 'redux';
import { organizerFilterReducer } from './organizer-filter';

export const reducer = combineReducers({
    organizerFilter: organizerFilterReducer
});
