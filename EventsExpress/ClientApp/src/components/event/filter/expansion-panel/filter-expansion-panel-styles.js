import { makeStyles } from '@material-ui/core/styles';

export const useFilterExpansionPanelStyles = makeStyles({
    heading: {
        margin: 0,
        borderWidth: 0,
        color: '#0c4bc7'
    },
    headingWrapper: {
        display: 'flex',
        alignItems: 'center',
        gap: '10px',
        position: 'relative'
    }
});
