import BookmarkService from '../../services/BookmarkService';
import { getUserInfo } from '../login/login-action';

const apiService = new BookmarkService();

export const saveBookmark = (userId, eventId) => {
    return async dispatch => {
        const response = await apiService.saveEventBookmark(userId, eventId);
        if (!response.ok) {
            return;
        }

        dispatch(getUserInfo());
    };
};

export const deleteBookmark = (userId, eventId) => {
    return async dispatch => {
        const response = await apiService.deleteEventBookmark(userId, eventId);
        if (!response.ok) {
            return;
        }

        dispatch(getUserInfo());
    };
};
