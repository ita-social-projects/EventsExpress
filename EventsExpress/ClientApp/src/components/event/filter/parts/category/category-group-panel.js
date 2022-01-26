import {
    FilterExpansionPanelDetails,
    FilterExpansionPanelSummary,
    FilterExpansionPanelWrapper
} from '../../expansion-panel/filter-expansion-panel-parts';
import React, { useState } from 'react';
import { Button } from '@material-ui/core';
import { useFilterExpansionPanelStyles } from '../../expansion-panel/filter-expansion-panel-styles';

export const CategoryGroupPanel = ({ title, children }) => {
    const [expanded, setExpanded] = useState(false);
    const classes = useFilterExpansionPanelStyles();

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
                <div className={classes.headingWrapper}>
                    <i className={`fas ${expanded ? 'fa-chevron-up' : 'fa-chevron-down'}`} />
                    <h6 className={classes.heading}>{title}</h6>
                </div>
            </FilterExpansionPanelSummary>
            <FilterExpansionPanelDetails>
                {children}
            </FilterExpansionPanelDetails>
        </FilterExpansionPanelWrapper>
    );
};