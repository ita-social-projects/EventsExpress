import React from 'react';
import './profile.css';
import EditUsernameContainer from '../../containers/editProfileContainers/editUsernameContainer';
import ExpansionPanel from '@material-ui/core/ExpansionPanel';
import ExpansionPanelDetails from '@material-ui/core/ExpansionPanelDetails';
import ExpansionPanelSummary from '@material-ui/core/ExpansionPanelSummary';
import Typography from '@material-ui/core/Typography';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import { makeStyles } from "@material-ui/core/styles";
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider'




const useStyles = makeStyles(theme => ({
    root: {
        width: '100%',
    },
    heading: {
        fontSize: theme.typography.pxToRem(15),
        flexBasis: '33.33%',
        flexShrink: 0,
    },
    secondaryHeading: {
        fontSize: theme.typography.pxToRem(15),
        color: theme.palette.text.secondary,
    },
}));




const Profile = (props) => {
    const classes = useStyles();

    const [expanded, setExpanded] = React.useState(false);

    const handleChange = panel => (event, isExpanded) => {
        setExpanded(isExpanded ? panel : false);
    };


    return (
        <div className={classes.root}>
            
                <ExpansionPanel expanded={expanded === 'panel1'} onChange={handleChange('panel1')}>
                    <ExpansionPanelSummary
                        expandIcon={<ExpandMoreIcon />}
                        aria-controls="panel1bh-content"
                        id="panel1bh-header"
                    >
                        <Typography className={classes.heading}>Username</Typography>
                    <Typography className={classes.secondaryHeading}>{props.user.Name}</Typography>
                    </ExpansionPanelSummary>
                <ExpansionPanelDetails>
                    
                    <Typography>
                        <MuiThemeProvider>
                            <EditUsernameContainer />
                        </MuiThemeProvider>
                    </Typography>

                </ExpansionPanelDetails>

            </ExpansionPanel>
            
        </div>
    );
}



export default Profile;
