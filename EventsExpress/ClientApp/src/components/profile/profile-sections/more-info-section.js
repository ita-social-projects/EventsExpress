import { makeStyles } from "@material-ui/core";
import React from "react";
import { Button } from '@material-ui/core';

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
                <h1>First block</h1>
            </div>
            <div className={classes.secondBlockContent}>
                <div className={classes.blockStyle}>
                    <h1>Second block</h1>
                </div>
                <div className={classes.blockStyle}>
                    <h1>Third block</h1>
                </div>
                <div className={classes.blockStyle}>
                    <h1>Fourth block</h1>
                </div>
                <div className={classes.blockStyle}>
                    <h1>Fiveth block</h1>
                </div>
            </div>
            <Button className={classes.editButton}>Edit</Button>
        </div>
    );
}