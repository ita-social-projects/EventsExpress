import React, { useContext, useState } from 'react';
import { Icon } from '@material-ui/core';
import { RefreshEventsContext } from '../quick-actions';
import { useSessionItem } from '../quick-actions-hooks';
import { BOOKMARKED_EVENTS_FILTER_APPLIED } from '../../../../constants/bookmarks-constants';
import { QuickActionButton } from '../quick-action-button';

export const BookmarkedEvents = () => {
    const bookmarkedSession = useSessionItem(BOOKMARKED_EVENTS_FILTER_APPLIED);
    const [bookmarked, setBookmarked] = useState(() => bookmarkedSession.value === 'true');
    const refreshEvents = useContext(RefreshEventsContext);

    const toggleBookmark = () => {
        bookmarkedSession.value = !bookmarked;
        setBookmarked(!bookmarked);
        refreshEvents();
    };

    return (
        <QuickActionButton
            key={+bookmarked}
            icon={bookmarked ? <Icon className="fas fa-bookmark" /> : <Icon className="far fa-bookmark" />}
            title={"Bookmarked events"}
            onClick={toggleBookmark}
        />
    );
};
