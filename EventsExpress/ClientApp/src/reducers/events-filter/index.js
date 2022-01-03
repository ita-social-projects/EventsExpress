import { combineReducers } from 'redux';
import { organizerFilterReducer } from './organizer-filter';
import { locationFilterReducer } from './location-filter';
export const reducer = combineReducers({
    organizerFilter: organizerFilterReducer,
    locationFilter: locationFilterReducer,
});
