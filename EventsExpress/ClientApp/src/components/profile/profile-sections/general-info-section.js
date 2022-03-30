import { makeStyles } from "@material-ui/core";
import React from "react";
import { Button } from '@material-ui/core';
import { InfoField } from "./info-field";
import { connect } from 'react-redux';
import DisplayLocation from '../../event/map/display-location';
import EditUsernameContainer from '../../../containers/editProfileContainers/editUsernameContainer';
import EditGenderContainer from '../../../containers/editProfileContainers/editGenderContainer';
import EditBirthdayContainer from '../../../containers/editProfileContainers/editBirthdayContainer';
import ChangeAvatarButton from "../editProfile/change-avatar-button";
import '../User-profile.css';
import CustomAvatar from "../../avatar/custom-avatar"
import ProfileAvatar from './profile-avatar'

const useStyles = makeStyles(theme => ({
    sectionContent: {
        position: "relative",
        display: "grid",
        gridTemplateRows: "1.8fr 2fr",
        height: "100%",
        width: "100%",
        gridGap: "20px",
    },
    firstBlockContent: {
        display: "grid",
        gridTemplateRows: "7fr 1fr",
        gridGap: "10px",
    },
    secondBlockContent: {
        display: "grid",
        backgroundColor: "gray",
    },
    blockStyle: {
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        border: "1px solid black"
    },
    editButton: {
        backgroundColor: "lightgray",
    },
    avatar :{
        border: "none"
    },

}));

const GeneralInfoSection = (props) => {
    const classes = useStyles();
    var birthday;
    if(props.birthday !== null){
        birthday = props.birthday.slice(0, 10);
    } 
    console.log(props);
    return (
        <div className={classes.sectionContent}>
            <div className={classes.firstBlockContent}>
                <div className={`${classes.blockStyle} ${classes.avatar}`}>
                    <div >
                        {/* <CustomAvatar height="300px" width="300px" name={props.name} userId={props.id}/> */}
                            <ProfileAvatar name={props.name} userId={props.id}></ProfileAvatar>
                    </div>
                </div>
               {/* <ChangeAvatarButton></ChangeAvatarButton> */}
            </div>
            <div className={classes.secondBlockContent}>
                <div className={classes.blockStyle}>
                    <h3>General information</h3>
                </div>
                <div className={classes.blockStyle}>
                    <InfoField fieldName={"Name"} info={props.name} editContainer={<EditUsernameContainer />} displayEditButton={true} />
                </div>
                <div className={classes.blockStyle}>
                    <InfoField fieldName={"Surname"} info={props.name} />
                </div>
                <div className={classes.blockStyle}>
                    <InfoField fieldName={"Date of birth"} info={birthday} editContainer={<EditBirthdayContainer />} displayEditButton={true} />
                </div>
                <div className={classes.blockStyle}>
                    <InfoField fieldName={"Gender"} info={props.gender ? 'Male' : 'Female'} editContainer={<EditGenderContainer />} displayEditButton={true} />
                </div>
                <div className={classes.blockStyle}>
                    <InfoField fieldName={"Location"} info={<DisplayLocation
                        location={props.location}
                    />} />
                </div>
                <div className={classes.blockStyle}>
                    <InfoField fieldName={"Email"} info={props.email} />
                </div>
            </div>
        </div>
    );
}

const mapStateToProps = state => {
    return state.user;
};

export default connect(mapStateToProps)(GeneralInfoSection);