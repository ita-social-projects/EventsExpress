import React from 'react';
import { connect } from 'react-redux';
import Moment from 'react-moment';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import ExpansionPanel from '@material-ui/core/ExpansionPanel';
import ExpansionPanelDetails from '@material-ui/core/ExpansionPanelDetails';
import ExpansionPanelSummary from '@material-ui/core/ExpansionPanelSummary';
import { makeStyles } from "@material-ui/core/styles";
import Typography from '@material-ui/core/Typography';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import EditUsernameContainer from '../../containers/editProfileContainers/editUsernameContainer';
import EditGenderContainer from '../../containers/editProfileContainers/editGenderContainer';
import EditBirthdayContainer from '../../containers/editProfileContainers/editBirthdayContainer';
import ChangePasswordContainer from '../../containers/editProfileContainers/changePasswordContainer';
import SelectCategoriesWrapper from '../../containers/categories/SelectCategories';
import genders from '../../constants/GenderConstants';
import ChangeAvatarWrapper from '../../containers/editProfileContainers/change-avatar';
import './profile.css';
import SelectNotificationTypesWrapper from '../../containers/notificationTypes/SelectNotificationTypes';
import LinkedAuthsWrapper from '../../containers/linked-auths-wrapper';
import { InterestsSection } from './profile-sections/interests-section';
import { MoreInfoSection } from './profile-sections/more-info-section';
import { GeneralInfoSection } from './profile-sections/general-info-section';

const useStyles = makeStyles(theme => ({
    root: {
        width: '100%',
        padding: "0px 50px 0px 30px",
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
    profileHeader: {
        width: "fit-content",
        margin: "10px 10px 10px 0px",
        padding: "0px 10px 0px 10px",
        border: "1px solid black",
    },
    profileContent: {
        display: "grid",
        gridTemplateColumns: "repeat(3, 1fr)",
        gridAutoRows: "350px",
        //gridTemplateRows: "repeat(2, 1fr)",
        gridRowGap: "30px",
        gridColumnGap: "40px",
    },
    interestsBlock: {
        backgroundColor: "gray",
        gridColumnStart: "1",
        gridColumnEnd: "3",
        gridRowStart: "1",
        gridRowEnd: "2",
    },
    moreInfoBlock: {
        backgroundColor: "gray",
        gridColumnStart: "1",
        gridColumnEnd: "3",
        gridRowStart: "2",
        gridRowEnd: "3",
    },
    generalInfoBlock: {
        gridColumnStart: "3",
        gridColumnEnd: "4",
        gridRowStart: "1",
        gridRowEnd: "3",
        width: "270px",
    },
}));

const Profile = (props) => {
    const classes = useStyles();
    const [expanded, setExpanded] = React.useState(false);

    const handleChange = panel => (event, isExpanded) => {
        setExpanded(isExpanded ? panel : false);
    };
    console.log(props);
    return (
        <div className={classes.root}>
            <div className={classes.profileHeader}>
                <h1>Profile</h1>
            </div>
            <div className={classes.profileContent}>
                <div className={classes.interestsBlock}>
                    <InterestsSection />
                </div>
                <div className={classes.moreInfoBlock}>
                    <MoreInfoSection />
                </div>
                <div className={classes.generalInfoBlock}>
                    <GeneralInfoSection />
                </div>
            </div>
            {/* <ExpansionPanel expanded={expanded === 'panel0'} onChange={handleChange('panel0')}>
                <ExpansionPanelSummary
                    expandIcon={<ExpandMoreIcon />}
                    aria-controls="panel1bh-content"
                    id="panel1bh-header"
                >
                    <Typography className={classes.heading}>Change Avatar</Typography>
                </ExpansionPanelSummary>
                <ExpansionPanelDetails>
                    <Typography className="w-100">
                        <MuiThemeProvider>
                            <ChangeAvatarWrapper />
                        </MuiThemeProvider>
                    </Typography>
                </ExpansionPanelDetails>
            </ExpansionPanel>
            <ExpansionPanel expanded={expanded === 'panel1'} onChange={handleChange('panel1')}>
                <ExpansionPanelSummary
                    expandIcon={<ExpandMoreIcon />}
                    aria-controls="panel1bh-content"
                    id="panel1bh-header"
                >
                    <Typography className={classes.heading}>Username</Typography>
                    <Typography className={classes.secondaryHeading}>{props.name}</Typography>
                </ExpansionPanelSummary>
                <ExpansionPanelDetails>
                    <Typography>
                        <MuiThemeProvider>
                            <EditUsernameContainer />
                        </MuiThemeProvider>
                    </Typography>
                </ExpansionPanelDetails>
            </ExpansionPanel>
            <ExpansionPanel expanded={expanded === 'panel2'} onChange={handleChange('panel2')}>
                <ExpansionPanelSummary
                    expandIcon={<ExpandMoreIcon />}
                    aria-controls="panel2bh-content"
                    id="panel2bh-header">
                    <Typography className={classes.heading}>Gender</Typography>
                    <Typography className={classes.secondaryHeading}>{genders[props.gender]}</Typography>
                </ExpansionPanelSummary>
                <ExpansionPanelDetails>
                    <Typography>
                        <MuiThemeProvider>
                            <EditGenderContainer />
                        </MuiThemeProvider>
                    </Typography>
                </ExpansionPanelDetails>
            </ExpansionPanel>
            <ExpansionPanel expanded={expanded === 'panel3'} onChange={handleChange('panel3')}>
                <ExpansionPanelSummary
                    expandIcon={<ExpandMoreIcon />}
                    aria-controls="panel3bh-content"
                    id="panel3bh-header"
                >
                    <Typography className={classes.heading}>Date of Birth</Typography>
                    <Typography className={classes.secondaryHeading}>
                        <Moment format="D MMM YYYY" withTitle>{props.birthday}</Moment>
                    </Typography>
                </ExpansionPanelSummary>
                <ExpansionPanelDetails>
                    <Typography>
                        <MuiThemeProvider>
                            <EditBirthdayContainer />
                        </MuiThemeProvider>
                    </Typography>
                </ExpansionPanelDetails>
            </ExpansionPanel>
            <ExpansionPanel expanded={expanded === 'panel4'} onChange={handleChange('panel4')}>
                <ExpansionPanelSummary
                    expandIcon={<ExpandMoreIcon />}
                    aria-controls="panel4bh-content"
                    id="panel4bh-header"
                >
                    <Typography className={classes.heading}>Favorite Categories</Typography>
                    <Typography className={classes.secondaryHeading}>
                        {props.categories.map(category => <div key={category.id}>{category.name}</div>)}
                    </Typography>
                </ExpansionPanelSummary>
                <ExpansionPanelDetails>
                    <Typography>
                        <MuiThemeProvider>
                            <SelectCategoriesWrapper />
                        </MuiThemeProvider>
                    </Typography>
                </ExpansionPanelDetails>
            </ExpansionPanel>
            <ExpansionPanel expanded={expanded === 'panel5'} onChange={handleChange('panel5')}>
                <ExpansionPanelSummary
                    expandIcon={<ExpandMoreIcon />}
                    aria-controls="panel5bh-content"
                    id="panel5bh-header"
                >
                    <Typography className={classes.heading}>Manage notifications</Typography>
                    <Typography className={classes.secondaryHeading}>
                        {props.notificationTypes.map(notificatin => <div key={notificatin.id}>{notificatin.name}</div>)}
                    </Typography>
                </ExpansionPanelSummary>
                <ExpansionPanelDetails>
                    <Typography>
                        <MuiThemeProvider>
                            <SelectNotificationTypesWrapper />
                        </MuiThemeProvider>
                    </Typography>
                </ExpansionPanelDetails>
            </ExpansionPanel>
            <ExpansionPanel expanded={expanded === 'panel6'} onChange={handleChange('panel6')}>
                <ExpansionPanelSummary
                    expandIcon={<ExpandMoreIcon />}
                    aria-controls="panel6bh-content"
                    id="panel6bh-header"
                >
                    <Typography className={classes.heading}>Linked accounts</Typography>
                </ExpansionPanelSummary>
                <ExpansionPanelDetails>
                    <Typography>
                        <MuiThemeProvider>
                            <LinkedAuthsWrapper />
                        </MuiThemeProvider>
                    </Typography>
                </ExpansionPanelDetails>
            </ExpansionPanel>
            {props.canChangePassword && <ChangePasswordContainer /> } */}
        </div>
    );
}

const mapStateToProps = state => {
    return state.user;
};

const mapDispatchToProps = dispatch => ({
    getComments: (data, page) => dispatch(getComments(data, page))
});

export default connect(mapStateToProps)(Profile);
