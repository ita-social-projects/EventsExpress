import React, { Component} from 'react';
import { makeStyles } from "@material-ui/core/styles";
import IconButton from "@material-ui/core/IconButton";
import Create from "@material-ui/icons/Create";
import Notifications from "@material-ui/icons/Notifications";
import DirectionsRun from "@material-ui/icons/DirectionsRun";
import AccountCircle from "@material-ui/icons/AccountCircle";
import Switch from "@material-ui/core/Switch";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import FormGroup from "@material-ui/core/FormGroup";
import MenuItem from "@material-ui/core/MenuItem";
import Menu from "@material-ui/core/Menu";
import Avatar from '@material-ui/core/Avatar';
import './header-profile.css';
import ReactDOM from 'react-dom';


const useStyles = makeStyles(theme => ({
    root: {
        flexGrow: 1
    },
    menuButton: {
        marginRight: theme.spacing(2)
    },
    title: {
        flexGrow: 1
    },
    bigAvatar: {
        margin: 10,
        width: 90,
        height: 90
    }
}));

export default function HeaderProfile() {
    const classes = useStyles();
    const [auth, setAuth] = React.useState(true);
    const [anchorEl, setAnchorEl] = React.useState(null);
    const open = Boolean(anchorEl);

    function handleChange(event) {
        setAuth(event.target.checked);
    }

    function handleMenu(event) {
        setAnchorEl(event.currentTarget);
    }

    function handleClose() {
        setAnchorEl(null);
    }

    return (
        <div className={classes.root}>
            <FormGroup>
                <FormControlLabel
                    control={
                        <Switch
                            checked={auth}
                            onChange={handleChange}
                            aria-label="LoginSwitch"
                        />
                    }
                    label={auth ? "Logout" : "Login"}
                />
            </FormGroup>

            <div>
                {!auth && (
                    <div>
                        <IconButton
                            aria-label="Account of current user"
                            aria-controls="menu-appbar"
                            aria-haspopup="true"
                            onClick={handleMenu}
                            color="#000000"
                        >
                            <AccountCircle />
                        </IconButton>
                        <Menu
                            id="menu-appbar"
                            anchorEl={anchorEl}
                            anchorOrigin={{
                                vertical: "top",
                                horizontal: "right"
                            }}
                            keepMounted
                            transformOrigin={{
                                vertical: "top",
                                horizontal: "right"
                            }}
                            open={open}
                            onClose={handleClose}
                        >
                            <MenuItem onClick={handleClose}>Sign In/Up</MenuItem>
                        </Menu>
                    </div>
                )}
                {auth && (
                    <div>
                        <Avatar
                            alt="Тут аватар"
                            src="/static/images/avatar/1.jpg"
                            className={classes.bigAvatar}
                        />
                        <h4>Тут Юзернейм</h4>
                        <h5>Тут стать</h5>
                        <h5>Тут Вік</h5>
                        <IconButton
                            aria-label="Account of current user"
                            aria-controls="menu-appbar"
                            aria-haspopup="true"
                            color="#000000"
                        >
                            <Create />
                        </IconButton>
                        <IconButton
                            aria-label="Account of current user"
                            aria-controls="menu-appbar"
                            aria-haspopup="true"
                            color="#000000"
                        >
                            <Notifications />
                        </IconButton>
                        <IconButton
                            className={classes.button}
                            aria-label="Edit"
                            aria-controls="menu-appbar"
                            aria-haspopup="true"
                            onClick={handleMenu}
                            color="#000000"
                        >
                            <DirectionsRun />
                        </IconButton>
                        <Menu
                            id="menu-appbar"
                            anchorEl={anchorEl}
                            anchorOrigin={{
                                vertical: "top",
                                horizontal: "right"
                            }}
                            keepMounted
                            transformOrigin={{
                                vertical: "top",
                                horizontal: "right"
                            }}
                            open={open}
                            onClose={handleClose}
                        >
                            <MenuItem onClick={handleClose}>Logout</MenuItem>
                        </Menu>
                    </div>
                )}
            </div>
        </div>
    );
}