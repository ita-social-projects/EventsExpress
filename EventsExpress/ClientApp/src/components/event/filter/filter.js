import React, { useState } from 'react';
import {
    Drawer,
    Icon,
    IconButton
} from '@material-ui/core';
import { useFilterStyles } from './filter-styles';
import FilterForm from './filter-form';

export const Filter = () => {
    const [open, setOpen] = useState(false);
    const classes = useFilterStyles();

    const toggleOpen = () => setOpen(!open);

    return (
        <div>
            <IconButton className={classes.openButton} onClick={toggleOpen}>
                <Icon className="fas fa-arrow-left" />
            </IconButton>
            <Drawer
                open={open}
                variant="persistent"
                anchor="right"
                classes={{ paper: classes.drawerPaper }}
            >
                <FilterForm toggleOpen={toggleOpen} />
            </Drawer>
        </div>
    );
};
