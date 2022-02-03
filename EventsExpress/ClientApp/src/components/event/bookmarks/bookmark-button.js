import Tooltip from '@material-ui/core/Tooltip';
import IconButton from '@material-ui/core/IconButton';
import React from 'react';
import { connect } from 'react-redux';
import { deleteBookmark, saveBookmark } from '../../../actions/bookmarks/event-bookmarks-actions';

const BookmarkButton = ({ event, currentUser, ...props }) => {
    const bookmarked = currentUser.bookmarkedEvents.includes(event.id);

    const toggleBookmark = () => {
        if (bookmarked) {
            props.deleteBookmark(currentUser.id, event.id);
        } else {
            props.saveBookmark(currentUser.id, event.id);
        }
    };

    return (
        <>
            {currentUser.id && (
                <Tooltip title="Bookmark">
                    <IconButton key={+bookmarked} onClick={toggleBookmark}>
                        {bookmarked ? <i className="fas fa-bookmark" /> : <i className="far fa-bookmark" />}
                    </IconButton>
                </Tooltip>
            )}
        </>
    );
};

const mapStateToProps = state => ({
    currentUser: state.user
});

const mapDispatchToProps = dispatch => ({
    saveBookmark: (userId, eventId) => dispatch(saveBookmark(userId, eventId)),
    deleteBookmark: (userId, eventId) => dispatch(deleteBookmark(userId, eventId))
});

export default connect(mapStateToProps, mapDispatchToProps)(BookmarkButton);
