import React, { useEffect, useState } from 'react';
import { IconButton, Tooltip, Menu, MenuItem } from '@material-ui/core';
import { eventOrderCriteriaEnum, ORDER } from '../../../../constants/event-order-criteria';
import SwapVertIcon from '@material-ui/icons/SwapVert';
import { useFilterActions } from '../../filter/filter-hooks';
import { useSessionItem } from '../quick-actions-hooks';
import { connect } from 'react-redux';
import { get_events } from '../../../../actions/event/event-list-action';

const OrderEvents = ({ getEvents }) => {
    const [menuAnchor, setMenuAnchor] = useState(null);
    const [orderCriteria, setOrderCriteria] = useState(eventOrderCriteriaEnum.START_SOON);

    const savedOrder = useSessionItem(ORDER);

    const { getQueryWithRequestFilters } = useFilterActions();

    useEffect(() => {
        setOrderCriteria(+savedOrder.value ?? eventOrderCriteriaEnum.START_SOON);
    }, []);

    useEffect(() => {
        savedOrder.value = orderCriteria;
        const query = getQueryWithRequestFilters();
        getEvents(query);
    }, [orderCriteria]);

    const changeCriteria = (event, criteria) => {
        event.stopPropagation();
        const newCriteria = (criteria === orderCriteria)
            ? eventOrderCriteriaEnum.START_SOON
            : criteria;
        setOrderCriteria(newCriteria);
    };

    return (
        <div>
            <Tooltip title="Order events">
                <IconButton
                    id="order-events-button"
                    aria-controls="order-events-menu"
                    aria-haspopup="true"
                    onClick={e => setMenuAnchor(e.currentTarget)}
                >
                    <SwapVertIcon />
                </IconButton>
            </Tooltip>
            <Menu
                id="order-events-menu"
                anchorEl={menuAnchor}
                getContentAnchorEl={null}
                open={Boolean(menuAnchor)}
                onClick={() => setMenuAnchor(null)}
                anchorOrigin={{ vertical: 'bottom' }}
            >
                <MenuItem
                    onClick={e => changeCriteria(e, eventOrderCriteriaEnum.START_SOON)}
                    selected={orderCriteria === eventOrderCriteriaEnum.START_SOON}
                >
                    Start soon
                </MenuItem>
                <MenuItem
                    onClick={e => changeCriteria(e, eventOrderCriteriaEnum.RECENTLY_PUBLISHED)}
                    selected={orderCriteria === eventOrderCriteriaEnum.RECENTLY_PUBLISHED}
                >
                    Recently published
                </MenuItem>
            </Menu>
        </div>
    );
};

const mapDispatchToProps = dispatch => ({
    getEvents: query => dispatch(get_events(query)),
});

export default connect(null, mapDispatchToProps)(OrderEvents);
