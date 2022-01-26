import React, { useState } from 'react';
import { connect } from 'react-redux';
import { IconButton, Tooltip, Menu, MenuItem } from '@material-ui/core';
import AssignmentTurnedInIcon from '@material-ui/icons/AssignmentTurnedIn';
import AssignmentTurnedInOutlinedIcon from '@material-ui/icons/AssignmentTurnedInOutlined';
import {
    get_events,
    getEventsToGoByUser,
    getVisitedEventsByUser,
} from '../../../../actions/event/event-list-action';

const JoinedEventsFilter = ({ userId, showEventsToGo, showVisitedEvents, resetEvents }) => {
    const [menuAnchor, setMenuAnchor] = useState(null);
    const [joinedEventsShown, setJoinedEventsShown] = useState(false);

    const switchJoinedEvents = event => {
        if (!joinedEventsShown) {
            setMenuAnchor(event.currentTarget);
        } else {
            resetEvents();
            setJoinedEventsShown(false);
        }
    };

    const closeMenu = () => {
        setMenuAnchor(null);
    };

    const goingToAttendClick = () => {
        showEventsToGo(userId);
        closeMenu();
        setJoinedEventsShown(true);
    };

    const archivedClick = () => {
        showVisitedEvents(userId);
        closeMenu();
        setJoinedEventsShown(true);
    };

    return (
        <div>
            <Tooltip title="Joined events">
                <IconButton
                    id="joined-events-button"
                    aria-controls="joined-events-menu"
                    aria-haspopup="true"
                    onClick={switchJoinedEvents}
                >
                    {joinedEventsShown ? (
                        <AssignmentTurnedInIcon />
                    ) : (
                        <AssignmentTurnedInOutlinedIcon />
                    )}
                </IconButton>
            </Tooltip>
            <Menu
                id="joined-events-menu"
                anchorEl={menuAnchor}
                getContentAnchorEl={null}
                open={Boolean(menuAnchor)}
                onClose={closeMenu}
                anchorOrigin={{ vertical: 'bottom' }}
            >
                <MenuItem onClick={goingToAttendClick}>
                    Going to attend
                </MenuItem>
                <MenuItem onClick={archivedClick}>
                    Archived
                </MenuItem>
            </Menu>
        </div>
    );
};

const mapStateToProps = state => ({
    userId: state.user.id,
});

const mapDispatchToProps = dispatch => ({
    showEventsToGo: userId => dispatch(getEventsToGoByUser(userId)),
    showVisitedEvents: userId => dispatch(getVisitedEventsByUser(userId)),
    resetEvents: () => dispatch(get_events('')),
});

export default connect(mapStateToProps, mapDispatchToProps)(JoinedEventsFilter);
