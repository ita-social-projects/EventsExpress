import { makeStyles } from "@material-ui/core";
import React from "react";

const useStyles = makeStyles(theme => ({
    fieldStyle: {
        width: "100%",
        
    },
    fieldNameStyle: {
        display: "inline-block",
        padding: "0px 0px 0px 5px",
    },
    infoBlockStyle: {
        display: "inline-block",
        padding: "0px 5px 0px 0px",
        float: "right",
    },
    fontStyle: {
        fontSize: "20px",
        margin: "0px",
        fontWeight: "bold",
    },
}));

export const InfoField = ({fieldName, info}) => {
    const classes = useStyles();

    return(
        <div className={classes.fieldStyle}>
            <div className={classes.fieldNameStyle}>
                <p className={classes.fontStyle}>{fieldName}</p>
            </div>
            <div className={classes.infoBlockStyle}>
                <p className={classes.fontStyle}>{info}</p>
            </div>
        </div>
    );
};