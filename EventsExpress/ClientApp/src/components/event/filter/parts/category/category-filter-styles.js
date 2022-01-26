import { makeStyles } from '@material-ui/core/styles';

export const useCategoryFilterStyles = makeStyles({
    wrapper: {
        display: 'flex',
        flexDirection: 'column',
        width: '100%',
        gap: '10px'
    },
    chips: {
        display: 'flex',
        flexWrap: 'wrap',
        gap: '10px'
    },
    fullWidth: {
        width: '100%'
    }
});