import { makeStyles } from "@material-ui/core";
import React from "react";
import { Button } from '@material-ui/core';
import { InfoField } from "./info-field";
import ChangeAvatarButton from "../editProfile/change-avatar-button";

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
    }
}));

export const GeneralInfoSection = () => {
    const classes = useStyles();

    return (
        <div className={classes.sectionContent}>
            <div className={classes.firstBlockContent}>
                <div className={classes.blockStyle}>
                    <h1>Picture</h1>
                </div>
               <ChangeAvatarButton></ChangeAvatarButton>
            </div>
            <div className={classes.secondBlockContent}>
                <div className={classes.blockStyle}>
                    <h3>General information</h3>
                </div>
                <div className={classes.blockStyle}>
                    <InfoField fieldName={"Name"} info={"test"} />
                </div>
                <div className={classes.blockStyle}>
                    <InfoField fieldName={"Surname"} info={"test"} />
                </div>
                <div className={classes.blockStyle}>
                    <InfoField fieldName={"Date of birth"} info={"test"} />
                </div>
                <div className={classes.blockStyle}>
                    <InfoField fieldName={"Gender"} info={"test"} />
                </div>
                <div className={classes.blockStyle}>
                    <InfoField fieldName={"Location"} info={"test"} />
                </div>
                <div className={classes.blockStyle}>
                    <InfoField fieldName={"Email"} info={"test"} />
                </div>
            </div>
        </div>
    );
}