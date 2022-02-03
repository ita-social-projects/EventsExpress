import React, { useContext, useEffect, useState } from 'react';
import { MenuItem } from '@material-ui/core';
import AssignmentTurnedInIcon from '@material-ui/icons/AssignmentTurnedIn';
import AssignmentTurnedInOutlinedIcon from '@material-ui/icons/AssignmentTurnedInOutlined';
import { RefreshEventsContext } from '../quick-actions';
import { useSessionItem } from '../quick-actions-hooks';
import { QuickActionButtonWithMenu } from '../quick-action-button-with-menu';
import { userToEventRelationEnum, DISPLAY_USER_EVENTS } from '../../../../constants/user-to-event-relation';

export const JoinedEventsFilter = () => {
    const [joinedEventsShown, setJoinedEventsShown] = useState(false);
    const refreshEvents = useContext(RefreshEventsContext);
    const displayUserEvents = useSessionItem(DISPLAY_USER_EVENTS);

    useEffect(() => {
        const filterApplied = Boolean(displayUserEvents.value);
        setJoinedEventsShown(filterApplied);
    }, []);

    const optionPairs = [
        [userToEventRelationEnum.GOING_TO_VISIT, 'Going to visit'],
        [userToEventRelationEnum.VISITED, 'Visited'],
    ];

    const handleItemClick = value => {
        displayUserEvents.value = value;
        setJoinedEventsShown(true);
        refreshEvents();
    };

    const renderFilterOptions = () => {
        return optionPairs.map(([key, value]) => (
            <MenuItem onClick={() => handleItemClick(key)}>
                {value}
            </MenuItem>
        ));
    };
    
    const restoreDefaultValues = () => {
        if (!joinedEventsShown) {
            return false;
        }
        displayUserEvents.reset();
        setJoinedEventsShown(false);
        refreshEvents();
        return true;
    };

    return (
        <QuickActionButtonWithMenu
            title="Joined events"
            icon={joinedEventsShown
                ? <AssignmentTurnedInIcon />
                : <AssignmentTurnedInOutlinedIcon />
            }
            renderMenuItems={renderFilterOptions}
            suppressMenu={restoreDefaultValues}
        />
    );
};
