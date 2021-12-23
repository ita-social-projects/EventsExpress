import { makeStyles } from '@material-ui/core/styles';

const drawerWidth = 320;
const drawerIndex = 250;

export const useFilterStyles = makeStyles({
    drawerPaper: {
        zIndex: drawerIndex,
        paddingTop: '53px',
        paddingBottom: '40px',
        width: drawerWidth,
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
        alignItems: 'center',
        padding: '0 10px',
        position: 'sticky',
        top: 0,
        backgroundColor: '#fff',
        zIndex: drawerIndex + 2
    },
    filterHeaderPart: {
        display: 'flex',
        alignItems: 'center',
        gap: '10px'
    },
    filterHeading: {
        margin: 0
    }
});
