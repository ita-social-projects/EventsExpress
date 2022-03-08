import { makeStyles } from "@material-ui/core";
import React from "react";
import { Button } from '@material-ui/core';

const useStyles = makeStyles(theme => ({
    sectionContent: {
        position: "relative",
        display: "grid",
        gridTemplateRows: "1.5fr 2fr",
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
                <Button className={classes.editButton}>Change your photo</Button>
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
        </div>
    );
}