import Tooltip from '@material-ui/core/Tooltip';
import IconButton from '@material-ui/core/IconButton';
import React from 'react';
import { connect } from 'react-redux';
import { deleteBookmark, saveBookmark } from '../../../actions/bookmarks/event-bookmarks-actions';

const BookmarkButton = ({ event, currentUser, ...props }) => {
    const bookmarked = currentUser.bookmarkedEvents.includes(event.id);

    const toggleBookmark = () => {
        if (bookmarked) {
            props.deleteBookmark(event.id);
        } else {
            props.saveBookmark(event.id);
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
    saveBookmark: eventId => dispatch(saveBookmark(eventId)),
    deleteBookmark: eventId => dispatch(deleteBookmark(eventId))
});

export default connect(mapStateToProps, mapDispatchToProps)(BookmarkButton);
