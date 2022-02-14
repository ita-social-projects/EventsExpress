import { withStyles } from '@material-ui/core/styles';
import { Accordion, AccordionDetails, AccordionSummary } from '@material-ui/core';

export const FilterExpansionPanelWrapper = withStyles({
    root: {
        boxShadow: 'none',
        margin: 0,
        '&:before': {
            display: 'none'
        },
        borderBottom: '1px solid #bbbbbb',
        '&$expanded': {
            margin: 0,
            borderBottom: 'none'
        }
    },
    expanded: {}
})(Accordion);

export const FilterExpansionPanelSummary = withStyles({
    root: {
        minHeight: 0,
        '&$expanded': {
            minHeight: 0
        }
    },
    content: {
        margin: '6px 0',
        '&$expanded': {
            margin: '6px 0'
        },
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'space-between'
    },
    expanded: {}
})(AccordionSummary);

export const FilterExpansionPanelDetails = withStyles({
    root: {}
})(AccordionDetails);
