import { makeStyles } from '@material-ui/core/styles';

const drawerWidth = 320;
const drawerIndex = 250;

export const useStyles = makeStyles(theme => ({
    drawerPaper: {
        zIndex: drawerIndex,
        top: '55px',
        width: drawerWidth
    },
    openButton: {
        zIndex: drawerIndex - 1,
        top: '56px',
        position: 'fixed',
        right: 0
    },
    filterHeader: {
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center'
    },
    filterHeading: {
        margin: 0
    },
    filterExpansionPanelHeading: {
        margin: 0,
        borderWidth: 0,
        color: '#0c4bc7'
    }
}));
