import { makeStyles } from "@material-ui/core";
import React from "react";
import { Button } from '@material-ui/core';

const useStyles = makeStyles(theme => ({
    sectionContent: {
        position: "relative",
        display: "grid",
        height: "100%",
        width: "100%",
        backgroundColor: "gray"
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

export const InterestsSection = () => {
    const classes = useStyles();

    return (
        <div className={classes.sectionContent}>
            <div className={classes.blockStyle}>
                <h1>Interest</h1>
            </div>
            <div className={classes.blockStyle}>
                <h1>Second block</h1>
            </div>
            <div className={classes.blockStyle}>
                <h1>Third block</h1>
            </div>
            <Button className={classes.editButton}>Edit</Button>
        </div>
    );
};