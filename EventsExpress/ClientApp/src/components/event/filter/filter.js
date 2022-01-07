import React, { useState } from 'react';
import { Drawer, Icon, IconButton, Typography } from '@material-ui/core';
import { useFilterStyles } from './filter-styles';
import FilterForm from './form/filter-form';

export const Filter = () => {
    const [open, setOpen] = useState(false);
    const classes = useFilterStyles();

    const toggleOpen = () => setOpen(!open);

    return (
        <div>
            <div className={classes.openButton}>
                <IconButton onClick={toggleOpen}>
                    <Icon className="fas fa-arrow-circle-left" />
                </IconButton>
                <Typography variant="h6" component="span">
                    Filters
                </Typography>
            </div>
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
