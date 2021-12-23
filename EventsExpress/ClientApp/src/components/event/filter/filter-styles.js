import { makeStyles } from '@material-ui/core/styles';

const drawerWidth = 320;
const drawerIndex = 250;

export const useFilterStyles = makeStyles({
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
    filterForm: {
    },
    filterContent: {
    },
    filterFooter: {
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        gap: '10px'
    }
});
