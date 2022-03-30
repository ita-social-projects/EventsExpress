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
import GeneralInfoSection from './profile-sections/general-info-section';
import SpinnerWrapper from "../../containers/spinner"

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
        width: "285px",
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
            <SpinnerWrapper
            showContent={props.id != undefined}
            >
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
            </SpinnerWrapper>
            
        </div>
    );
}

const mapStateToProps = state => {
    return state.user;
};


export default connect(mapStateToProps)(Profile);
