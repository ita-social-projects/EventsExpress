import React, { useContext, useEffect, useState } from 'react';
import { MenuItem } from '@material-ui/core';
import SwapVertIcon from '@material-ui/icons/SwapVert';
import { useSessionItem } from '../quick-actions-hooks';
import { QuickActionButtonWithMenu } from '../quick-action-button-with-menu';
import { eventOrderCriteriaEnum, ORDER } from '../../../../constants/event-order-criteria';
import { RefreshEventsContext } from '../quick-actions';

export const OrderEvents = () => {
    const [orderCriteria, setOrderCriteria] = useState(eventOrderCriteriaEnum.START_SOON);
    const refreshEvents = useContext(RefreshEventsContext);
    const savedOrder = useSessionItem(ORDER);

    useEffect(() => {
        setOrderCriteria(+savedOrder.value ?? eventOrderCriteriaEnum.START_SOON);
    }, []);

    const criteriaPairs = [
        [eventOrderCriteriaEnum.START_SOON, 'Start soon'],
        [eventOrderCriteriaEnum.RECENTLY_PUBLISHED, 'Recently published'],
    ];
    
    const handleItemClick = (event, criteria) => {
        event.stopPropagation();
        const newCriteria = (criteria === orderCriteria)
            ? eventOrderCriteriaEnum.START_SOON
            : criteria;
        setOrderCriteria(newCriteria);
        savedOrder.value = orderCriteria;
        refreshEvents();
    };

    const renderCriteria = () => {
        return criteriaPairs.map(([key, value]) => (
            <MenuItem
                onClick={e => handleItemClick(e, key)}
                selected={orderCriteria === key}
            >
                {value}
            </MenuItem>
        ));
    };

    return (
        <QuickActionButtonWithMenu
            title="Order events"
            icon={<SwapVertIcon />}
            renderMenuItems={renderCriteria}
        />
    );
};
