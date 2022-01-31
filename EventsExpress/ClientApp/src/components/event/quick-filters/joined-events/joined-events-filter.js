import React, { useEffect, useState } from 'react';
import { IconButton, Tooltip, Menu, MenuItem } from '@material-ui/core';
import AssignmentTurnedInIcon from '@material-ui/icons/AssignmentTurnedIn';
import AssignmentTurnedInOutlinedIcon from '@material-ui/icons/AssignmentTurnedInOutlined';
import { userToEventRelation } from '../../../../constants/user-to-event-relation-enum';
import { useSessionFilter } from '../quick-filter-hooks';

export const JoinedEventsFilter = () => {
    const [menuAnchor, setMenuAnchor] = useState(null);
    const [joinedEventsShown, setJoinedEventsShown] = useState(false);

    const joinedEventsFilter = useSessionFilter('displayUserEvents');

    useEffect(() => {
        const filterApplied = Boolean(joinedEventsFilter.value);
        setJoinedEventsShown(filterApplied);
    }, []);

    const switchJoinedEvents = event => {
        if (!joinedEventsShown) {
            setMenuAnchor(event.currentTarget);
        } else {
            joinedEventsFilter.reset();
            setJoinedEventsShown(false);
        }
    };

    const showEventsGoingToVisit = () => {
        joinedEventsFilter.value = userToEventRelation.GOING_TO_VISIT;
        setJoinedEventsShown(true);
    };

    const showVisitedEvents = () => {
        joinedEventsFilter.value = userToEventRelation.VISITED;
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
                    {joinedEventsShown
                        ? (
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
                onClick={() => setMenuAnchor(null)}
                anchorOrigin={{ vertical: 'bottom' }}
            >
                <MenuItem onClick={showEventsGoingToVisit}>
                    Going to visit
                </MenuItem>
                <MenuItem onClick={showVisitedEvents}>
                    Visited
                </MenuItem>
            </Menu>
        </div>
    );
};
