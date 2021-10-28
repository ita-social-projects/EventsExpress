import initialState from '../../store/initialState';
import { GET_CATEGORY_GROUPS_DATA } from '../../actions/categoryGroup/category-group-list-action';

export const reducer = (state = initialState.categoryGroups, action) => {
    switch (action.type) {
        case GET_CATEGORY_GROUPS_DATA:
            return {
                ...state,
                data: action.payload,
            }
        default:
            return state;
    }
}