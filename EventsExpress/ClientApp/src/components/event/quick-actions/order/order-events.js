import React, { useContext, useEffect, useState } from 'react';
import { MenuItem } from '@material-ui/core';
import SwapVertIcon from '@material-ui/icons/SwapVert';
import SwapVerticalCircleIcon from '@material-ui/icons/SwapVerticalCircle';
import { useSessionItem } from '../quick-actions-hooks';
import { QuickActionButtonWithMenu } from '../quick-action-button-with-menu';
import { eventOrderCriteriaEnum, ORDER } from '../../../../constants/event-order-criteria';
import { RefreshEventsContext } from '../quick-actions';

const criteriaPairs = [
    [eventOrderCriteriaEnum.START_SOON, 'Start soon'],
    [eventOrderCriteriaEnum.RECENTLY_PUBLISHED, 'Recently published'],
];

const defaultOrder = eventOrderCriteriaEnum.START_SOON;

export const OrderEvents = () => {
    const [orderCriteria, setOrderCriteria] = useState(defaultOrder);
    const refreshEvents = useContext(RefreshEventsContext);
    const savedOrder = useSessionItem(ORDER);

    useEffect(() => {
        setOrderCriteria(+savedOrder.value ?? defaultOrder);
    }, []);
    
    const handleItemClick = (event, criteria) => {
        event.stopPropagation();
        let newCriteria = criteria;
        if (criteria === orderCriteria) {
            if (criteria === defaultOrder) {
                return;
            }
            newCriteria = defaultOrder;
        }
        setOrderCriteria(newCriteria);
        savedOrder.value = newCriteria;
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
            icon={orderCriteria === defaultOrder
                ? <SwapVertIcon />
                : <SwapVerticalCircleIcon />
            }
            renderMenuItems={renderCriteria}
        />
    );
};
