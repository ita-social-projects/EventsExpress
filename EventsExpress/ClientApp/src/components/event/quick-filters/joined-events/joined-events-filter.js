import React, { useEffect, useState } from 'react';
import { IconButton, Tooltip, Menu, MenuItem } from '@material-ui/core';
import AssignmentTurnedInIcon from '@material-ui/icons/AssignmentTurnedIn';
import AssignmentTurnedInOutlinedIcon from '@material-ui/icons/AssignmentTurnedInOutlined';
import { useFilterActions, useFilterInitialValues } from '../../filter/filter-hooks';

export const JoinedEventsFilter = () => {
    const [menuAnchor, setMenuAnchor] = useState(null);
    const [joinedEventsShown, setJoinedEventsShown] = useState(false);

    const { goingToVisit, visited } = useFilterInitialValues();
    const { appendFilters } = useFilterActions();

    useEffect(() => {
        setJoinedEventsShown(goingToVisit || visited);
    }, []);

    const switchJoinedEvents = event => {
        if (!joinedEventsShown) {
            setMenuAnchor(event.currentTarget);
        } else {
            appendFilters({ goingToVisit: null, visited: null });
            setJoinedEventsShown(false);
        }
    };

    const closeMenu = () => {
        setMenuAnchor(null);
    };

    const showEventsGoingToVisit = () => {
        appendFilters({ goingToVisit: true });
        closeMenu();
        setJoinedEventsShown(true);
    };

    const showVisitedEvents = () => {
        appendFilters({ visited: true });
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
