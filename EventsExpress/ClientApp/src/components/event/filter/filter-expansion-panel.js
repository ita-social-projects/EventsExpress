import {
    FilterExpansionPanelDetails,
    FilterExpansionPanelSummary,
    FilterExpansionPanelWrapper
} from './filter-expansion-panel-parts';
import React, { useState } from 'react';
import { useStyles } from './filter-styles';
import { Button } from '@material-ui/core';

export const FilterExpansionPanel = ({ title, children }) => {
    const [expanded, setExpanded] = useState(false);
    const classes = useStyles();

    const handleChange = panel => (event, newExpanded) => {
        if (event.target.localName === 'button' || event.target.localName === 'span') {
            return;
        }

        setExpanded(newExpanded ? panel : false);
    };

    return (
        <FilterExpansionPanelWrapper
            square
            expanded={expanded}
            onChange={handleChange(true)}
        >
            <FilterExpansionPanelSummary
                key={expanded}
                aria-controls="panel1d-content"
                id="panel1d-header"
            >
                <div style={{ display: 'flex', alignItems: 'center', gap: '10px' }}>
                    <i className={`fas ${expanded ? 'fa-chevron-up' : 'fa-chevron-down'}`} />
                    <h6 className={classes.filterExpansionPanelHeading}>{title}</h6>
                </div>
                <Button color="secondary" size="small">Clear</Button>
            </FilterExpansionPanelSummary>
            <FilterExpansionPanelDetails>
                {children}
            </FilterExpansionPanelDetails>
        </FilterExpansionPanelWrapper>
    );
};
