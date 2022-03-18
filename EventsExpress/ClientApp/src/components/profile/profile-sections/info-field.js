import { makeStyles } from "@material-ui/core";
import React from "react";
import { Button } from '@material-ui/core';
import { useState } from 'react';
import { IconButton } from '@material-ui/core';
import EditIcon from '@material-ui/icons/Edit';

const useStyles = makeStyles(theme => ({
    fieldStyle: {
        width: "100%",
        position: "relative",
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
    editButtonStyle: {
        position: "absolute",
        right: "-45px",
        padding: "10px",
        top: "-5px",
        //display: "none",
    },
    editFieldStyle: {
        padding: "5px",
    }
}));



export const InfoField = ({fieldName, info, editContainer, displayEditButton = false}) => {
    const classes = useStyles();

    const [isOpen, setIsOpen] = useState(false);

    return(
        <div className={classes.fieldStyle}>
            {!isOpen &&
                <div className={classes.fieldNameStyle}>
                    <p className={classes.fontStyle}>{fieldName}</p>
                </div>
            }
            {!isOpen &&            
                <div className={classes.infoBlockStyle}>
                    <p className={classes.fontStyle}>{info}</p>
                </div>
            }
            {displayEditButton &&
                <IconButton className={classes.editButtonStyle} onClick={() => {isOpen ? setIsOpen(false) : setIsOpen(true)}}>
                    <EditIcon />
                </IconButton>
            }
            {isOpen &&
                <div className={classes.editFieldStyle}>
                    {editContainer}
                </div>
            }
        </div>
    );
};