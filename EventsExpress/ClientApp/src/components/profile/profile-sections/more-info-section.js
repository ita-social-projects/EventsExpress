import { makeStyles } from "@material-ui/core";
import React from "react";
import { Button } from '@material-ui/core';
import { InfoField } from "./info-field";

const useStyles = makeStyles(theme => ({
    sectionContent: {
        position: "relative",
        display: "grid",
        gridTemplateRows: "1fr 2fr",
        height: "100%",
        width: "100%",
        backgroundColor: "gray"
    },
    secondBlockContent: {
        display: "grid",
    },
    blockStyle: {
        display: "flex",
        alignItems: "center",
        justifyContent: "center",
        border: "1px solid black"
    },
    editButton: {
        position: "absolute",
        top: "30px",
        right: "10px",
        backgroundColor: "lightgray",
    }
}));

export const MoreInfoSection = () => {
    const classes = useStyles();

    return (
        <div className={classes.sectionContent}>
            <div className={classes.blockStyle}>
                <h1>More information</h1>
            </div>
            <div className={classes.secondBlockContent}>
                <div className={classes.blockStyle}>
                    <InfoField fieldName={"Relationship status"} info={"test"} />
                </div>
                <div className={classes.blockStyle}>
                    <InfoField fieldName={"Parents status"} info={"test"} />
                </div>
                <div className={classes.blockStyle}>
                    <InfoField fieldName={"Type of leisure"} info={"test"} />
                </div>
                <div className={classes.blockStyle}>
                    <InfoField fieldName={"Reasons for using the site"} info={"test"} />
                </div>
                <div className={classes.blockStyle}>
                    <InfoField fieldName={"Type of event"} info={"test"} />
                </div>
                <div className={classes.blockStyle}>
                    <InfoField fieldName={"Tell us more about yourself"} info={"test"} />
                </div>
            </div>
            <Button className={classes.editButton}>Edit</Button>
        </div>
    );
}