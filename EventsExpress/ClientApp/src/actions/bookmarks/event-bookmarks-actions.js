import BookmarkService from '../../services/BookmarkService';
import { getUserInfo } from '../login/login-action';

const apiService = new BookmarkService();

export const saveBookmark = eventId => {
    return async dispatch => {
        const response = await apiService.saveEventBookmark(eventId);
        if (!response.ok) {
            return;
        }

        dispatch(getUserInfo());
    };
};

export const deleteBookmark = eventId => {
    return async dispatch => {
        const response = await apiService.deleteEventBookmark(eventId);
        if (!response.ok) {
            return;
        }

        dispatch(getUserInfo());
    };
};
