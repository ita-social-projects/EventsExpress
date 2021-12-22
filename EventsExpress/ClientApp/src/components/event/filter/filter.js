import React, { useState } from 'react';
import {
    Button,
    Drawer,
    Icon,
    IconButton,
} from '@material-ui/core';
import { useStyles } from './filter-styles';

export const Filter = () => {
    const [open, setOpen] = useState(false);
    const classes = useStyles();

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
                <div className={classes.filterHeader}>
                    <IconButton onClick={toggleOpen}>
                        <Icon className="fas fa-arrow-right" />
                    </IconButton>
                    <h5 className={classes.filterHeading}>Filters</h5>
                    <Button color="secondary">Clear</Button>
                </div>
            </Drawer>
        </div>
    );
};
