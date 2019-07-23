import initialState from '../store/initialState';
import {SET_USER} from '../actions/login';
import {SET_LOGOUT} from '../actions/logout';

import { addUserCategory } from '../actions/EditProfile/addUserCategory';
import { editBirthday } from '../actions/EditProfile/editBirthday';
import { editGender } from '../actions/EditProfile/EditGender';
import { editUsername } from '../actions/EditProfile/editUsername';



export const reducer = (state, action) => {
    state = state || initialState.user;
    switch(action.type)
        {
            case SET_USER:
                return action.payload;

            case SET_LOGOUT:
                return initialState.user;

            case addUserCategory.UPDATE:
                    return {
                        ...state,
                        categories: action.payload.Categories
                    }
            case editBirthday.UPDATE:
                    return {
                        ...state,
                        birthday: action.payload.Birthday
                    }
            case editUsername.UPDATE:
                    return {
                        ...state,
                        name: action.payload.UserName
                    }

            case editGender.UPDATE:
                return {
                    ...state,
                    gender: action.payload.Gender
                }
        }
    return state;
}