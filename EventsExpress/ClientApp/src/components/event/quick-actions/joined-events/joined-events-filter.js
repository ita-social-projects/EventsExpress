import React, { useEffect, useState } from 'react';
import { connect } from 'react-redux';
import { IconButton, Tooltip, Menu, MenuItem } from '@material-ui/core';
import { userToEventRelationEnum, DISPLAY_USER_EVENTS } from '../../../../constants/user-to-event-relation';
import { useSessionFilter } from '../quick-actions-hooks';
import { useFilterActions } from '../../filter/filter-hooks';
import { get_events } from '../../../../actions/event/event-list-action';
import AssignmentTurnedInIcon from '@material-ui/icons/AssignmentTurnedIn';
import AssignmentTurnedInOutlinedIcon from '@material-ui/icons/AssignmentTurnedInOutlined';

const JoinedEventsFilter = ({ getEvents }) => {
    const [menuAnchor, setMenuAnchor] = useState(null);
    const [joinedEventsShown, setJoinedEventsShown] = useState(false);

    const joinedEventsFilter = useSessionFilter(DISPLAY_USER_EVENTS);

    const { getQueryWithRequestFilters } = useFilterActions();

    useEffect(() => {
        const filterApplied = Boolean(joinedEventsFilter.value);
        setJoinedEventsShown(filterApplied);
    }, []);

    useEffect(() => {
        const filterQuery = getQueryWithRequestFilters();
        getEvents(filterQuery);
    }, [joinedEventsShown]);

    const switchJoinedEvents = event => {
        if (!joinedEventsShown) {
            setMenuAnchor(event.currentTarget);
        } else {
            joinedEventsFilter.reset();
            setJoinedEventsShown(false);
        }
    };

    const showEventsGoingToVisit = () => {
        joinedEventsFilter.value = userToEventRelationEnum.GOING_TO_VISIT;
        setJoinedEventsShown(true);
    };

    const showVisitedEvents = () => {
        joinedEventsFilter.value = userToEventRelationEnum.VISITED;
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

const mapDispatchToProps = dispatch => ({
    getEvents: query => dispatch(get_events(query)),
});

export default connect(null, mapDispatchToProps)(JoinedEventsFilter);
